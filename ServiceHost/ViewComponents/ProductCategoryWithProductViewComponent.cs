using _01_ShopQuery.Contracts.ProductCategory;
using _01_ShopQuery.Query;
using Microsoft.AspNetCore.Mvc;
namespace ServiceHost.ViewComponents
{
    public class ProductCategoryWithProductViewComponent : ViewComponent
    {
        private readonly IProductCategoryQuery _productCategoryQuery;
        public ProductCategoryWithProductViewComponent(IProductCategoryQuery productCategoryQuery)
        {
            _productCategoryQuery = productCategoryQuery;
        }
        public IViewComponentResult Invoke()
        {
            var categories = _productCategoryQuery.GetProductCategoryWithProducts();
            return View(categories);
        }
    }
}
