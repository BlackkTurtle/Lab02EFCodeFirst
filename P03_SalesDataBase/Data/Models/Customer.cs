namespace P03_SalesDataBase.Data.Models
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string CreditCardNumber { get; set; }
        public virtual ICollection<Sale> Sales { get; } = new List<Sale>();
    }
}
