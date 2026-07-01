using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.ProductAgg;
using ShopManagement.Application.Contracts.ProductCategoryAgg;
using System.Collections.Generic;
using System.Linq;

namespace ServiceHost.Areas.Administration.Pages.Shop.Products
{
    public class IndexModel : PageModel
    {
        public List<ProductViewModel> Products { get; set; }
        public ProductSearchModel SearchModel { get; set; }
        public SelectList ProductCategories { get; set; }
        public SelectList Stock { get; set; }

        private readonly IProductApplication _productApplication;
        private readonly IProductCategoryApplication _productCategoryApplication;


        public IndexModel(IProductApplication productApplication, IProductCategoryApplication categoryApplication)
        {
            _productCategoryApplication = categoryApplication;
            _productApplication = productApplication;
        }

        public void OnGet(ProductSearchModel searchModel)
        {
            Dictionary<int, string> inStock = new Dictionary<int, string>();


            inStock.Add(1, "ãæÌæÏ");
            inStock.Add(2, "äÇãæÌæÏ");

            ProductCategories = new SelectList(_productCategoryApplication.GetProductCategories(), "Id", "Name");

            Stock = new SelectList(inStock.Select(item => new
            {
                Id = item.Key,
                Name = item.Value
            }), "Id", "Name");

            Products = _productApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            var command = new CreateProduct()
            {
                ProductCategories = _productCategoryApplication.GetProductCategories()
            };
            return Partial("./Create", command);
        }

        public IActionResult OnPostCreate(CreateProduct command)
        {
            var result = _productApplication.Create(command);
            return new JsonResult(result);
        }
        public IActionResult OnGetEdit(long id)
        {
            var command = _productApplication.GetDetails(id);
            command.ProductCategories = _productCategoryApplication.GetProductCategories();

            return Partial("./Edit", command);
        }

        public IActionResult OnPostEdit(EditProduct command)
        {
            var resul = _productApplication.Edit(command);
            return new JsonResult(resul);
        }

        public IActionResult OnGetNotInStock(long id)
        {
            _productApplication.NotInstok(id);
            return RedirectToPage("./Index");
        }
        public IActionResult OnGetInStock(long id)
        {
            _productApplication.Instok(id);
            return RedirectToPage("./Index");
        }
    }
}
