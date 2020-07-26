using Microsoft.VisualStudio.TestTools.UnitTesting;
using Store.Domain.Commands.Interfaces;
using Store.Domain.Entities;
using Store.Domain.Enums;
using Store.Domain.Queries;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Store.Tests
{
    [TestClass]
    public class CreateOrderCommandTest
    {
       

        [TestMethod]
        [TestCategory("Queries")]
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

       

    }
}
