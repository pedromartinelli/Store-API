using Flunt.Notifications;

namespace Store.Domain.Entities
{
    public abstract class Entity : Notifiable<Notification>
    {
        public Entity()
        {
            Id = new Guid();
        }

        public Guid Id { get; private set; }
    }
}
