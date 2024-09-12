using Store.Domain.Entities;
using Store.Domain.Repositories;

namespace Store.Tests.Repositories
{
    public class FakeProductRepository : IProductRepository
    {
        public IEnumerable<Product> Get(IEnumerable<Guid> ids)
        {
            IList<Product> products = [];
            products.Add(new Product("Memória ram", 230, true));
            products.Add(new Product("Placa mãe", 650, true));
            products.Add(new Product("Placa de vídeo", 2200, true));
            products.Add(new Product("NVME", 310, true));

            return products;
        }
    }
}
