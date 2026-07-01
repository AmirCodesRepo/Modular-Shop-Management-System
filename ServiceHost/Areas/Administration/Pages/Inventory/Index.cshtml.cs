using InventoryManagement.Application.Contracts.InventoryAgg;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.ProductAgg;
using System.Collections.Generic;

namespace ServiceHost.Areas.Administration.Pages.Inventory
{
    public class IndexModel : PageModel
    {
        public List<InventoryViewModel> Inventory { get; set; }
        public InventorySearchModel SearchModel { get; set; }
        public SelectList Products { get; set; }

        private readonly IInventoryApplication _inventoryApplication;
        private readonly IProductApplication _productApplication;
        public IndexModel(IInventoryApplication inventoryApplication, IProductApplication productApplication)
        {
            _inventoryApplication = inventoryApplication;
            _productApplication = productApplication;
        }

        public void OnGet(InventorySearchModel searchModel)
        {
            Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
            Inventory = _inventoryApplication.Search(searchModel);
        }
        public IActionResult OnGetOperationLog(long id)
        {
            var commadn = _inventoryApplication.GetOperationLog(id);
            return Partial("./OperationLog",commadn);
        }

        public IActionResult OnGetCreate()
        {
            var command = new CreateInventory
            {
                Products = _productApplication.GetProducts()
            };
            return Partial("./Create", command);
        }

        public IActionResult OnPostCreate(CreateInventory command)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToPage("./Create", command);
            }
            var result = _inventoryApplication.Create(command);
            return new JsonResult(result);
        }
        public IActionResult OnGetEdit(long id)
        {
            var command = _inventoryApplication.GetDetails(id);
            command.Products = _productApplication.GetProducts();


            return Partial("./Edit", command);
        }

        public IActionResult OnPostEdit(EditInventory command)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToPage("./Edit", command);
            }
            var resul = _inventoryApplication.Edit(command);
            return new JsonResult(resul);
        }
        public IActionResult OnGetIncrease(long id)
        {
            var command = new IncreaseInventory
            {
                InventoryId = id
            };
            return Partial("./Increase",command);
        }
        public IActionResult OnPostIncrease(IncreaseInventory command)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToPage("./Increase", command);
            }
            var resul = _inventoryApplication.Increase(command);
            return new JsonResult(resul);
        }
        public IActionResult OnGetReduce(long id)
        {
            var command = new ReduceInventory
            {
                InventoryId = id
            };
            return Partial("./Reduce", command);
        }
        public IActionResult OnPostReduce(ReduceInventory command)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToPage("./Reduce", command);
            }
            var resul = _inventoryApplication.Reduce(command);
            return new JsonResult(resul);
        }
    }
}
