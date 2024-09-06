using Store.Domain.Entities;
using Store.Domain.Enums;

namespace Store.Tests.Entities
{
    [TestClass]
    public class OrderTests
    {
        private readonly Customer _customer = new("Pedro", "pedro@email.com");
        private readonly Product _product = new("Placa mãe", 850.99M, true);
        private readonly Discount _discount = new(10, DateTime.Now.AddDays(5));

        [TestMethod]
        [TestCategory("Domain")]
        public void GivenANewValidOrderItShouldGenerateANumberWith8Caracteres()
        {
            var order = new Order(_customer, 0, null!);
            Assert.AreEqual(8, order.Number.Length);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void GivenANewOrderStatusShouldBeWaitingPayment()
        {
            var order = new Order(_customer, 0, null!);
            Assert.AreEqual(order.Status, EOrderStatus.WaitingPayment);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void GivenAPaymentOrderStatusShouldBeWaitingDelivery()
        {
            var order = new Order(_customer, 0, null!);
            order.AddItem(_product, 1);
            order.Pay(850.99M);
            Assert.AreEqual(order.Status, EOrderStatus.WaitingDelivery);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void GivenACanceledPaymentOrderStatusShouldBeCanceled()
        {
            var order = new Order(_customer, 0, null!);
            order.Cancel();
            Assert.AreEqual(order.Status, EOrderStatus.Canceled);
        }
    }
}
