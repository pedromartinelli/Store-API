using Store.Domain.Commands;

namespace Store.Tests.Commands
{
    [TestClass]
    public class CreateOrderCommandTests
    {
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
                Quantity = 4
            },
        ];

        [TestMethod]
        [TestCategory("Commands")]
        public void GivenAInvalidCommandOrderShouldNotBeCreated()
        {
            var command = new CreateOrderCommand("11111111111", "29102-02", "BOIA10", _items);
            command.Validate();
            Assert.AreEqual(command.IsValid, false);
        }
    }
}
