using _01_ShopQuery.Contracts.ProductCategory;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace ServiceHost.ViewComponents
{
    public class MenuViewComponent : ViewComponent
    {
        private readonly IProductCategoryQuery _productCategoryQuery;

        public MenuViewComponent(IProductCategoryQuery productCategoryQuery)
        {
            _productCategoryQuery = productCategoryQuery;
        }

        public IViewComponentResult Invoke()
        {
            var categories = _productCategoryQuery.GetProductCategoriesForMenu();
            return View(categories);
        }
    }
}
