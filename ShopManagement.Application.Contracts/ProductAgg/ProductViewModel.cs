namespace ShopManagement.Application.Contracts.ProductAgg
{
    public class ProductViewModel
    {
        public long Id { set; get; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public string Code { get; set; }
        public bool IsInStock { get; set; }
        public string Category { get; set; }
        public string CreationDate { get; set; }
    }
}
