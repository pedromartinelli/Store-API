using Flunt.Extensions.Br.Validations;
using Flunt.Notifications;
using Flunt.Validations;
using Store.Domain.Commands.Interfaces;
using Store.Domain.Entities;

namespace Store.Domain.Commands
{
    public class CreateOrderCommand : Notifiable<Notification>, ICommand
    {
        public CreateOrderCommand()
        {
        }

        public CreateOrderCommand(string customer, string zipCode, string discountCode, IList<CreateOrderItemCommand> items)
        {
            Customer = customer;
            ZipCode = zipCode;
            DiscountCode = discountCode;
            Items = items;
        }

        public string Customer { get; set; } = null!;
        public string ZipCode { get; set; } = null!;
        public string? DiscountCode { get; set; } = null!;
        public IList<CreateOrderItemCommand> Items { get; set; } = [];

        public void Validate()
        {
            AddNotifications(new Contract<Order>()
                .Requires()
                .AreEquals(Customer, 11, "Customer", "Cliente inválido")
                .IsZipCode(ZipCode, "ZipCode", "Cep inválido")
                );
        }

        public IList<Guid> ExtractItemsGuid()
        {
            var guids = new List<Guid>();
            guids.AddRange(Items.Select(i => i.Product));
            return guids;
        }
    }
}
