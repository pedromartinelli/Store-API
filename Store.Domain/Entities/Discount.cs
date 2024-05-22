namespace Store.Domain.Entities
{
    public class Discount : Entity
    {
        public decimal Amount { get; private set; }
        public DateTime ExpireDate { get; private set; }

        public bool IsValid()
            => DateTime.Compare(DateTime.Now, ExpireDate) < 0;

        public decimal Value()
        {
            if (IsValid()) return Amount;
            return 0;
        }
    }
}

