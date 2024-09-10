using Store.Domain.Entities;
using Store.Domain.Enums;

namespace Store.Tests.Entities
{
    [TestClass]
    public class OrderTests
    {
        private readonly Customer _customer = new("Pedro", "pedro@email.com");
        private readonly Product _product = new("Placa mãe", 850.99M, true);
        private readonly Product _product2 = new("Pasta Térmica", 10.00M, true);
        private readonly Discount _discount = new(10, DateTime.Now.AddDays(5));
        private readonly Discount _expiredDiscount = new(10, DateTime.Now.AddDays(-5));

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

        [TestMethod]
        [TestCategory("Domain")]
        public void GivenANewItemWithoutAProductItShouldNotBeenAdded()
        {
            var order = new Order(_customer, 0, null!);
            order.AddItem(null!, 1);
            Assert.AreEqual(order.Items.Count, 0);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void GivenANewItemWithQuantityEqualsZeroItShouldNotBeenAdded()
        {
            var order = new Order(_customer, 0, null!);
            order.AddItem(_product, 0);
            Assert.AreEqual(order.Items.Count, 0);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void GivenAValidDiscountOrderTotalMustBe50()
        {
            var order = new Order(_customer, 0, _discount);
            order.AddItem(_product2, 6);
            Assert.AreEqual(order.Total(), 50);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void GivenAExpiredDiscountOrderTotalMustBe60()
        {
            var order = new Order(_customer, 10, _expiredDiscount);
            order.AddItem(_product2, 5);
            Assert.AreEqual(order.Total(), 60);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void GivenAInvalidDiscountOrderTotalMustBe60()
        {
            var order = new Order(_customer, 10, null!);
            order.AddItem(_product2, 5);
            Assert.AreEqual(order.Total(), 60);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void GivenADeliveryFeeOf10OrderTotalMustBe60()
        {
            var order = new Order(_customer, 10, _discount);
            order.AddItem(_product2, 6);
            Assert.AreEqual(order.Total(), 60);
        }

        [TestMethod]
        [TestCategory("Domain")]
        public void GivenAOrderWithoutCustomerItShouldBeInvalid()
        {
            var order = new Order(null!, 10, _discount);
            Assert.AreEqual(order.IsValid, false);
        }
    }
}
