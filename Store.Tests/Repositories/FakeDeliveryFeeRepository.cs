using Store.Domain.Repositories;

namespace Store.Tests.Repositories
{
    public class FakeDeliveryFeeRepository : IDeliveryFeeRepository
    {
        public decimal Get(string zipCode)
        {
            if (zipCode == "11111111")
                return 12.57M;

            if (zipCode == "22222222")
                return 15.99M;

            if (zipCode == "33333333")
                return 35;

            return 0;
        }
    }
}
