using Microsoft.VisualStudio.TestTools.UnitTesting;
using Store.Domain.Entities;
using Store.Domain.Enums;
using System;

namespace Store.Tests
{
    [TestClass]
    public class OrderTests
    {
        private readonly Customer _customer = new Customer("Vanessa Camargo", "vanessa@gmail.com");

        private readonly Product _product = new Product("Produto1", 10, true);

        private readonly Discount _discount = new Discount(10, DateTime.Now.AddDays(5));

        [TestMethod]
        [TestCategory("Domain")]
        public void Dado_um_pedido_valido_ele_deve_gerar_um_numero_com_8_caracteres()
        {

            var order = new Order(_customer, 0, null);
            Assert.AreEqual(8, order.Number.Length);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void Dado_um_pedido_pedido_seu_status_deve_ser_aguardando_pagamento()
        {
            var order = new Order(_customer, 0, null);
            Assert.AreEqual(order.Status, EOrderStatus.WaitingPayment);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void Dado_um_pagamento_do_seu_status_deve_ser_aguardando_entrega()
        {
            var order = new Order(_customer, 0, null);
            order.AddItem(_product, 1);
            order.Pay(10);
            Assert.AreEqual(order.Status, EOrderStatus.WaitingDelivery);
        }
    }

}
