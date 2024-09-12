using Flunt.Notifications;
using Flunt.Validations;
using Store.Domain.Commands.Interfaces;
using Store.Domain.Entities;

namespace Store.Domain.Commands
{
    public class CreateOrderItemCommand : Notifiable<Notification>, ICommand
    {
        public CreateOrderItemCommand()
        {
        }

        public CreateOrderItemCommand(Guid product, int quantity)
        {
            Product = product;
            Quantity = quantity;
        }

        public Guid Product { get; set; }
        public int Quantity { get; set; }

        public void Validate()
        {
            AddNotifications(new Contract<OrderItem>()
                .Requires()
                .AreEquals(Product.ToString(), 32, "Product", "Produto inválido, id inconsistente")
                .IsGreaterThan(Quantity, 0, "Quantity", "Quantidade inválida, deve ser maior que 0")
                );
        }

    }
}
