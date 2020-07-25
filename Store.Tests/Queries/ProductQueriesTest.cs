using Microsoft.VisualStudio.TestTools.UnitTesting;
using Store.Domain.Entities;
using Store.Domain.Enums;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Store.Tests
{
    [TestClass]
    public class ProductQueriesTest
    {
        private IList<Product> _products;

        [TestMethod]
        [TestCategory("Queries")]
        public void Dado_a_consulta_de_produto_ativo_deve_retornar_3()
        {

            _products = new List<Product>();
            _products.Add(new Product("produto 01", 10, true));
            _products.Add(new Product("produto 02", 10, true));
            _products.Add(new Product("produto 03", 10, true));
            _products.Add(new Product("produto 04", 10, false));
            _products.Add(new Product("produto 05", 10, false));
        }

    }
}
