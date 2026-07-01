using DiscountManagement.Application.Contracts.CustomerDiscountAgg;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.ProductAgg;
using System.Collections.Generic;

namespace ServiceHost.Areas.Administration.Pages.Discount.CustomersDiscount
{
    public class IndexModel : PageModel
    {
        public List<CustomerDiscountViewModel> CustomerDicounts { get; set; }
        public CustomerDiscountSearchModel SearchModel { get; set; }
        public SelectList Products { get; set; }

        private readonly ICustomerDiscountApplication _customerDiscountApplication;
        private readonly IProductApplication _productApplication;
        public IndexModel(ICustomerDiscountApplication customerDiscountApplication, IProductApplication productApplication)
        {
            _customerDiscountApplication = customerDiscountApplication;
            _productApplication = productApplication;
        }

        public void OnGet(CustomerDiscountSearchModel searchModel)
        {
            Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
            CustomerDicounts = _customerDiscountApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            var command = new DefineCustomerDiscount
            {
                Proudcts = _productApplication.GetProducts()
            };
            return Partial("./Create", command);
        }

        public IActionResult OnPostCreate(DefineCustomerDiscount command)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToPage("./Create", command);
            }
            var result = _customerDiscountApplication.Define(command);
            return new JsonResult(result);
        }
        public IActionResult OnGetEdit(long id)
        {
            var command = _customerDiscountApplication.GetDetailes(id);
            command.Proudcts = _productApplication.GetProducts();


            return Partial("./Edit", command);
        }

        public IActionResult OnPostEdit(EditCustomerDiscount command)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToPage("./Edit", command);
            }
            var resul = _customerDiscountApplication.Eidt(command);
            return new JsonResult(resul);
        }
    }
}
