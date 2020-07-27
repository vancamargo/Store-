using Flunt.Notifications;
using Store.Domain.Commands;
using Store.Domain.Commands.Interfaces;
using Store.Domain.Entities;
using Store.Domain.Handlers.Interfaces;
using Store.Domain.Repositories;
using Store.Domain.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace Store.Domain.Handlers
{
    public class OrderHandler : Notifiable, IHandler<CreateOrderCommand>
    {
        private readonly ICustomerRespository _customerRepository;
        private readonly IDeliveryFeeRepository _deliveryFeeRepository;
        private readonly IDiscountRepository _discountRepository;
        private readonly IProductRepository _productRespository;
        private readonly IOrderRepository _orderRepository;

        public OrderHandler(ICustomerRespository customerRepository, 
            IDeliveryFeeRepository deliveryFeeRepository, 
            IDiscountRepository discountRepository,
            IProductRepository productRespository, 
            IOrderRepository orderRepository)
        {
            _customerRepository = customerRepository;
            _deliveryFeeRepository = deliveryFeeRepository;
            _discountRepository = discountRepository;
            _productRespository = productRespository;
            _orderRepository = orderRepository;
        }

        public ICommandResult Handle(CreateOrderCommand command)
        {
            //fail fast Validation
            command.Validate();
            if (command.Invalid)
                return new GenericCommandResult(false, "Pedido inválido", command.Notifications);

            // 1. Recuper o cliente 

            var customer = _customerRepository.Get(command.Customer);

            // 2. Calcula a taxa de entrega

            var deliveryFee = _deliveryFeeRepository.Get(command.ZipCode);

            // 3. Obtém o cupum de desconto

            var discount = _discountRepository.Get(command.PromoCode);

            //Gera o pedido
            var products = _productRespository.Get(ExtractGuids.Extract(command.Items)).ToList();
            var order = new Order(customer, deliveryFee, discount);
            foreach (var item in command.Items)
            {
                var product = products.Where(x => x.Id == item.Product).FirstOrDefault();
                order.AddItem(product, item.Quantity);
            }

            //agrupa as notificações
            AddNotifications(customer.Notifications);

            //verifica se deu tudo certo
            if (Invalid)
                return new GenericCommandResult(false, "Falha ao Gera o Pedido", Notifications);
            //Retorna o resultado

            return new GenericCommandResult(true, $"pedido {order.Number} gerado com sucesso", order);
        }
    }
}
