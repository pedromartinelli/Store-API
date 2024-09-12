using Flunt.Notifications;
using Store.Domain.Commands;
using Store.Domain.Commands.Interfaces;
using Store.Domain.Entities;
using Store.Domain.Handlers.Interfaces;
using Store.Domain.Repositories;

namespace Store.Domain.Handlers
{
    public class OrderHandler(ICustomerRepository customerRepository,
        IDeliveryFeeRepository deliveryFeeRepository, IDiscountRepository discountRepository,
        IOrderRepository orderRepository, IProductRepository productRepository) : Notifiable<Notification>, IHandler<CreateOrderCommand>
    {
        private readonly ICustomerRepository _customerRepository = customerRepository;
        private readonly IDeliveryFeeRepository _deliveryFeeRepository = deliveryFeeRepository;
        private readonly IDiscountRepository _discountRepository = discountRepository;
        private readonly IOrderRepository _orderRepository = orderRepository;
        private readonly IProductRepository _productRepository = productRepository;

        public ICommandResult Handle(CreateOrderCommand command)
        {
            command.Validate();
            if (!command.IsValid)
                return new GenericCommandResult(false, "Pedido inválido", command.Notifications);

            var customer = _customerRepository.Get(command.Customer);
            var deliveryFee = _deliveryFeeRepository.Get(command.ZipCode);
            var discount = _discountRepository.Get(command.DiscountCode!);
            var products = _productRepository.Get(command.ExtractItemsGuid());

            var order = new Order(customer, deliveryFee, discount);

            foreach (var item in command.Items)
            {
                var product = products.Where(x => x.Id == item.Product).FirstOrDefault();
                order.AddItem(product!, item.Quantity);
            }

            AddNotifications(order.Notifications);

            if (!IsValid)
                return new GenericCommandResult(false, "Falha ao gerar pedido", Notifications);

            _orderRepository.Save(order);
            return new GenericCommandResult(true, $"Pedido {order.Number} gerado com sucesso", order);
        }
    }
}
