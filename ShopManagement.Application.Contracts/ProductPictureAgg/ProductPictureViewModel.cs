namespace ShopManagement.Application.Contracts.ProductPictureAgg
{
    public class ProductPictureViewModel
    {
        public long Id { get; set; }
        public string Picture { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }
        public string Product { get; set; }
        public bool IsRemoved { get; set; }
        public string CreationDate { get; set; }
    }
}
