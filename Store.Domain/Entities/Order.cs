using Flunt.Validations;
using Store.Domain.Enums;

namespace Store.Domain.Entities
{
    public class Order : Entity
    {
        private readonly IList<OrderItem> _items;

        public Order(Customer customer, decimal deliveryFee, Discount discount)
        {
            AddNotifications(
                new Contract<Order>()
                    .Requires()
                    .IsNotNull(customer, "Customer", "Cliente inválido")
                );

            Customer = customer;
            Date = DateTime.Now;
            Number = Guid.NewGuid().ToString()[..8];
            _items = [];
            DeliveryFee = deliveryFee;
            Status = EOrderStatus.WaitingPayment;
            Discount = discount;
        }

        public Customer Customer { get; private set; }
        public DateTime Date { get; private set; }
        public string Number { get; private set; }
        public IReadOnlyCollection<OrderItem> Items => _items.ToArray();
        public decimal DeliveryFee { get; private set; }
        public Discount Discount { get; private set; }
        public EOrderStatus Status { get; private set; }

        public void AddItem(Product product, int quantity)
        {
            var item = new OrderItem(product, quantity);
            if (item.IsValid) _items.Add(item);
        }

        public decimal Total()
        {
            decimal total = 0;
            foreach (var item in Items)
            {
                total += item.Total();
            }

            total += DeliveryFee;
            total -= Discount != null ? Discount.Value() : 0;

            return total;
        }

        public void Pay(decimal amount)
        {
            if (amount == Total()) Status = EOrderStatus.WaitingDelivery;
        }

        public void Cancel()
            => Status = EOrderStatus.Canceled;
    }
}
