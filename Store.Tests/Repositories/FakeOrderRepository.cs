using Store.Domain.Entities;
using Store.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Store.Tests.Repositories
{
    public class FakeOrderRepository : IProductRepository
    {
        public IEnumerable<Product> Get(IEnumerable<Guid> ids)
        {
            IList<Product> products = new List<Product>();
            products.Add(new Product("produto 01", 10, true));
            products.Add(new Product("produto 02", 10, true));
            products.Add(new Product("produto 03", 10, true));
            products.Add(new Product("produto 04", 10, false));
            products.Add(new Product("produto 05", 10, false));

            return products;
        }
    }
}
