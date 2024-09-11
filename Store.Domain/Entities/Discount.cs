namespace Store.Domain.Entities
{
    public class Discount : Entity
    {
        public Discount(string code, decimal amount, DateTime expireDate)
        {
            Code = code;
            Amount = amount;
            ExpireDate = expireDate;
        }

        public string Code { get; private set; }
        public decimal Amount { get; private set; }
        public DateTime ExpireDate { get; private set; }

        private new bool IsValid()
            => DateTime.Compare(DateTime.Now, ExpireDate) < 0;

        public decimal Value()
        {
            if (IsValid()) return Amount;
            return 0;
        }
    }
}

