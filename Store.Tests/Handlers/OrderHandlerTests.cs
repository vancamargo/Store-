using Microsoft.VisualStudio.TestTools.UnitTesting;
using Store.Domain.Commands.Interfaces;
using Store.Domain.Entities;
using Store.Domain.Enums;
using Store.Domain.Handlers;
using Store.Domain.Queries;
using Store.Domain.Repositories;
using Store.Tests.Repositories;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Store.Tests
{
    [TestClass]
    public class OrderHandlerTests
    {
        private readonly ICustomerRespository _customerRepository;
        private readonly IDeliveryFeeRepository _deliveryFeeRepository;
        private readonly IDiscountRepository _discountRepository;
        private readonly IProductRepository _productRespository;
        private readonly IOrderRepository _orderRepository;

        public OrderHandlerTests()
        {
            _customerRepository = new FakeCustomerRespository();
            _deliveryFeeRepository = new FakeDeliveryFeeRepository();
            _discountRepository = new FakeDiscountRepository();
            _orderRepository = new FakeOrderReposistory();
            _productRespository = new FakeProductRepository();

        }

        [TestMethod]
        [TestCategory("Handlers")]
        public void Dado_um_cliente_inexistente_o_pedido_nao_deve_ser_gerado()
        {
            var command = new CreateOrderCommand();
            command.Customer = "teste";
            command.ZipCode = "13411080";
            command.PromoCode = "12345678";
            command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
            command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));

            var handler = new OrderHandler(_customerRepository, _deliveryFeeRepository,
                _discountRepository, _productRespository, _orderRepository);
            handler.Handle(command);
            Assert.AreEqual(handler.Valid, true);
        }

        [TestMethod]
        [TestCategory("Handlers")]
        public void Dado_um_cep_invalido_o_pedido_deve_ser_gerado_normalmente()
        {
            var command = new CreateOrderCommand();
            command.Customer = "12254663";
            command.ZipCode = "142568";
            command.PromoCode = "12345678";
            command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
            command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));

            var handler = new OrderHandler(_customerRepository, _deliveryFeeRepository,
                _discountRepository, _productRespository, _orderRepository);
            handler.Handle(command);
            Assert.AreEqual(handler.Valid, false);
        }

        [TestMethod]
        [TestCategory("Handlers")]
        public void Dado_um_comando_invalido_o_pedido_nao_deve_ser_gerado()
        {
            var command = new CreateOrderCommand();
            command.Customer = "";
            command.ZipCode = "13411080";
            command.PromoCode = "12345678";
            command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
            command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
            command.Validate();

            Assert.AreEqual(command.Valid, false);
        }

        [TestMethod]
        [TestCategory("Handlers")]
        public void Dado_um_comando_valido_o_pedido_deve_ser_gerado()
        {
            var command = new CreateOrderCommand();
            command.Customer = "1234568";
            command.ZipCode = "13411080";
            command.PromoCode = "12345678";
            command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
            command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));

            var handler = new OrderHandler(_customerRepository, _deliveryFeeRepository, 
                _discountRepository, _productRespository, _orderRepository);
            handler.Handle(command);
            Assert.AreEqual(handler.Valid, true);
        }

        [TestMethod]
        [TestCategory("Handlers")]
        public void Dado_um_promocode_inexistente_o_pedido_deve_ser_gerado_normalmente()
        {
            var command = new CreateOrderCommand();
            command.Customer = "1234568";
            command.ZipCode = "13411080";
            command.PromoCode = "123";
            command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));
            command.Items.Add(new CreateOrderItemCommand(Guid.NewGuid(), 1));

            var handler = new OrderHandler(_customerRepository, _deliveryFeeRepository,
               _discountRepository, _productRespository, _orderRepository);
            handler.Handle(command);
            Assert.AreEqual(handler.Valid, true);

        }



    }
}
