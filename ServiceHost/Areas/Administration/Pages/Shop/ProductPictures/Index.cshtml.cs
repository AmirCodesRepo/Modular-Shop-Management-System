using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.ProductAgg;
using ShopManagement.Application.Contracts.ProductCategoryAgg;
using ShopManagement.Application.Contracts.ProductPictureAgg;
using ShopManagement.Domain.ProductPictureAgg;
using System.Collections.Generic;
using System.Linq;

namespace ServiceHost.Areas.Administration.Pages.Shop.ProductPictures
{
    public class IndexModel : PageModel
    {
        public List<ProductPictureViewModel> ProductPictures { get; set; }
        public long ProductId { get; set; }

        private readonly IProductApplication _productApplication;
        private readonly IProductPictureApplication _productPictureApplication;


        public IndexModel(IProductApplication productApplication, IProductPictureApplication productPictureApplication)
        {
            _productPictureApplication = productPictureApplication;
            _productApplication = productApplication;
        }

        public void OnGet(long id)
        {
            ProductPictures = _productPictureApplication.GetProductPictures(id);
            ProductId = id;
        }

        public IActionResult OnGetCreate(long id)
        {
            var command = new CreateProductPicture
            {
                ProductId = id
            };
            return Partial("./Create", command);
        }

        public IActionResult OnPostCreate(CreateProductPicture command)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToPage("./Create", command);
            }
            var result = _productPictureApplication.Create(command);
            return new JsonResult(result);
        }
        public IActionResult OnGetEdit(long id)
        {
            var command = _productPictureApplication.GetDetails(id);
            //{
            //    command.ProductCategories = _productCategoryApplication.ProductCategories();
            //};

            return Partial("./Edit", command);
        }

        public IActionResult OnPostEdit(EditProductPicture command)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToPage("./Edit", command);
            }
            var resul = _productPictureApplication.Edit(command);
            return new JsonResult(resul);
        }

        public IActionResult OnGetRemove(long id,long p)
        {
            _productPictureApplication.Remove(id);
            return RedirectToPage("./index", new {id=p});
        }
        public IActionResult OnGetRestore(long id,long p)
        {
            _productPictureApplication.Restore(id);
            return RedirectToPage("./index",new {id = p});
        }
    }
}
