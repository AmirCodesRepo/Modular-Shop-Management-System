using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.ProductCategoryAgg;
using System.Collections.Generic;

namespace ServiceHost.Areas.Administration.Pages.Shop.ProductCategories
{
    public class IndexModel : PageModel
    {
        public List<ProductCategoryViewModel> ProductCategories { get; set; }
        public ProductCategorySearchModel SearchModel { get; set; }

        private readonly IProductCategoryApplication _productCategoryApplication;

        public IndexModel(IProductCategoryApplication productCategoryApplication)
        {
            _productCategoryApplication = productCategoryApplication;
        }
        public void OnGet(ProductCategorySearchModel searchModel)
        {
            ProductCategories = _productCategoryApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            return Partial("./Create", new CreateProductCategory());
        }
        public IActionResult OnPostCreate(CreateProductCategory command)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToPage("./Create",command);
            }
            var result = _productCategoryApplication.Create(command);
            return new JsonResult(result);
        }
        public IActionResult OnGetEdit(long id)
        {
            var productCategory = _productCategoryApplication.GetDetails(id);
            return Partial("./Edit", productCategory);
        }

        public IActionResult OnPostEdit(EditProductCategory command)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToPage("./Edit", command);
            }
            var resul = _productCategoryApplication.Edit(command);
            return new JsonResult(resul);
        }
    }
}
