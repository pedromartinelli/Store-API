using Store.Domain.Commands;
using Store.Domain.Handlers;
using Store.Domain.Repositories;

namespace Store.Tests.Handlers
{
    [TestClass]
    public class OrderHandlerTests(ICustomerRepository customerRepository, IDeliveryFeeRepository deliveryFeeRepository, IDiscountRepository discountRepository, IOrderRepository orderRepository, IProductRepository productRepository)
    {
        private readonly ICustomerRepository _customerRepository = customerRepository;
        private readonly IDeliveryFeeRepository _deliveryFeeRepository = deliveryFeeRepository;
        private readonly IDiscountRepository _discountRepository = discountRepository;
        private readonly IOrderRepository _orderRepository = orderRepository;
        private readonly IProductRepository _productRepository = productRepository;
        private readonly IList<CreateOrderItemCommand> _items =
        [
            new CreateOrderItemCommand
            {
                Product = new Guid(),
                Quantity = 3
            },
            new CreateOrderItemCommand
            {
                Product = new Guid(),
                Quantity = 0
            },
        ];

        [TestMethod]
        [TestCategory("Handlers")]
        public void GivenAValidCommandTheOrderMustBeGenerated()
        {
            var command = new CreateOrderCommand("11111111111", "29102-023", "BOIA10", _items);

            var handler = new OrderHandler(
                _customerRepository,
                _deliveryFeeRepository,
                _discountRepository,
                _orderRepository,
                _productRepository
             );

            handler.Handle(command);

            Assert.Equals(handler.IsValid, true);
        }
    }
}
