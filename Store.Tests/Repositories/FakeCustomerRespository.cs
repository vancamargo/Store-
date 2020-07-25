using Store.Domain.Entities;
using Store.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
//Mocks

namespace Store.Tests.Repositories
{
    public class FakeCustomerRespository : ICustomerRespository
    {
        public Customer Get(string document)
        {
            if (document == "12345678911")
                return new Customer("Bruce wayne", "batman@balta.io");
            return null;
        }
    }
}
