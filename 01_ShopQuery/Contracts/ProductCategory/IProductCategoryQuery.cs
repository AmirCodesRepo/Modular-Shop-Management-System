using System.Collections.Generic;

namespace _01_ShopQuery.Contracts.ProductCategory
{
    public interface IProductCategoryQuery
    {
        List<ProductCategoryQueryModel> GetProductCategoriesForMenu();
        List<ProductCategoryQueryModel> GetProductCategories();
        List<ProductCategoryQueryModel> GetProductCategoryWithProducts();
        ProductCategoryQueryModel GetProductCategoryWithProductsBy(string slug);
    }
}
