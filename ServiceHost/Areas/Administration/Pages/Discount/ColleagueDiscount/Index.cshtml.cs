using DiscountManagement.Application.Contracts.ColleagueDiscountAgg;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.ProductAgg;
using System.Collections.Generic;

namespace ServiceHost.Areas.Administration.Pages.DiscountManagement.ColleagueDiscount
{
    public class IndexModel : PageModel
    {
        public List<ColleagueDiscountViewModel> ColleagueDicounts { get; set; }
        public ColleagueDiscountSearchModel SearchModel { get; set; }
        public SelectList Products { get; set; }

        private readonly IColleagueDiscountApplication _colleagueDiscountApplication;
        private readonly IProductApplication _productApplication;
        public IndexModel(IColleagueDiscountApplication colleagueDiscountApplication, IProductApplication productApplication)
        {
            _colleagueDiscountApplication = colleagueDiscountApplication;
            _productApplication = productApplication;
        }

        public void OnGet(ColleagueDiscountSearchModel searchModel)
        {
            Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
            ColleagueDicounts = _colleagueDiscountApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            var command = new DefineColleagueDiscount
            {
                Products = _productApplication.GetProducts()
            };
            return Partial("./Create", command);
        }

        public IActionResult OnPostCreate(DefineColleagueDiscount command)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToPage("./Create", command);
            }
            var result = _colleagueDiscountApplication.Define(command);
            return new JsonResult(result);
        }
        public IActionResult OnGetEdit(long id)
        {
            var command = _colleagueDiscountApplication.GetDetailes(id);
            command.Products = _productApplication.GetProducts();


            return Partial("./Edit", command);
        }

        public IActionResult OnPostEdit(EditColleagueDiscounnt command)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToPage("./Edit", command);
            }
            var resul = _colleagueDiscountApplication.Edit(command);
            return new JsonResult(resul);
        }
        public IActionResult OnGetRemove(long id)
        {
            _colleagueDiscountApplication.Remove(id);
            return RedirectToPage("./Index");
        }
        public IActionResult OnGetRestore(long id)
        {
            _colleagueDiscountApplication.Restore(id);
            return RedirectToPage("./Index");
        }
    }
}
