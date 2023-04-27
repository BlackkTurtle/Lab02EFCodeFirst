namespace P03_SalesDataBase.Data.Models
{
    public class Store
    {
        public int StoreId { get; set; }
        public string Name { get; set; }
        public virtual ICollection<Sale> Sales { get; } = new List<Sale>();
    }
}
