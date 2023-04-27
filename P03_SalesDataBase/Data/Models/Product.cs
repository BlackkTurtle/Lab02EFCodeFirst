namespace P03_SalesDataBase.Data.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public double Quantity { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Sale> Sales { get; } = new List<Sale>();
    }
}
