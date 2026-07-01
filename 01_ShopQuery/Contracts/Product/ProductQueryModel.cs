using System.Collections.Generic;

namespace _01_ShopQuery.Contracts.Product
{
    public class ProductQueryModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }
        public string Category { get; set; }
        public string CategorySlug { get; set; }
        public string Price { get; set; }
        public string shortDescription { get; set; }
        public string PriceWhitDiscount { get; set; }
        public int DiscountRate { get; set; }
        public string Slug { get; set; }
        public bool HasDiscount { get; set; }
        public bool IsNew { get; set; }
        public string DiscountExpireDate { get; set; }


        public string Code { get; set; }
        public bool IsInStock { get; set; }
        public string Description { get; set; }
        public string Keywords { get; set; }
        public string MetaDescription { get; set; }

        public List<ProductPictureQueryModel> ProductPictures { get; set; }
        public List<ProeuctCommentQueryModel> ProeuctComments { get; set; }
    }

    public class ProductPictureQueryModel
    {
        public long ProductId { get; set; }
        public string Picture { get; set; }
        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }
        public bool IsRemoved { get; set; }
    }

    public class ProeuctCommentQueryModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }
        public string ProductSlug { get; set; }
        public long ProductId { get; set; }
    }
}
