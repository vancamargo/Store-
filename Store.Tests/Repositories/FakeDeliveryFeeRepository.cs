using Store.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Store.Tests.Repositories
{
    class FakeDeliveryFeeRepository : IDeliveryFeeRepository
    {
        public decimal Get(string zipCode)
        {
            return 10;
        }
    }
}
