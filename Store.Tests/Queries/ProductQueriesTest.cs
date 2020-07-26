using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    public class CreateOrderTests
    {
        private IList<Product> _products;

        [TestMethod]
        [TestCategory("Queries")]
        public void Dado_a_consulta_de_produto_ativo_deve_retornar_3()
        {

            _products = new List<Product>();
            _products.Add(new Product("produto 01", 10, true));
            _products.Add(new Product("produto 02", 20, true));
            _products.Add(new Product("produto 03", 30, true));
            _products.Add(new Product("produto 04", 40, false));
            _products.Add(new Product("produto 05", 50, false));
        }

        [TestMethod]
        [TestCategory("Queries")]
        public void Dado_a_consulta_de_produtos_ativos_deve_retorna_3()
        {
            var result = _products.AsQueryable().Where(ProductQueries.GetActiveProducts());
            Assert.AreEqual(result.Count(), 3);
        }

        [TestMethod]
        [TestCategory("Queries")]
        public void Dado__a_consulta_de_produtos_inativos_deve_retornar_2()
        {
            var result = _products.AsQueryable().Where(ProductQueries.GetInactiveProducts());
            Assert.AreEqual(result.Count(), 2);
        }

    }
}
