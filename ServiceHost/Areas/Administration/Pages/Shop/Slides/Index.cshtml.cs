using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.Contracts.SlideAgg;
using System.Collections.Generic;
using System.Linq;

namespace ServiceHost.Areas.Administration.Pages.Shop.Slides
{
    public class IndexModel : PageModel
    {
        public List<SlideViewModel> Slides { get; set; }
        public SlideSearchModel SearchModel { get; set; }
        public SelectList Status { get; set; }

        private readonly ISlideApplication _slideApplication;

        public IndexModel(ISlideApplication slideApplication)
        {
            _slideApplication = slideApplication;
        }

        public void OnGet(SlideSearchModel searchModel)
        {
            Dictionary<int, string> IsRemoved = new Dictionary<int, string>();

            IsRemoved.Add(1, "ÛíÑÝÚÇá");
            IsRemoved.Add(2, "ÝÚÇá");

            Status = new SelectList(IsRemoved.Select(item => new
            {
                Id = item.Key,
                Name = item.Value
            }), "Id", "Name");

            Slides = _slideApplication.Search(searchModel);
        }
        public IActionResult OnGetCreate()
        {
            return Partial("./Create");
        }
        public IActionResult OnPostCreate(CreateSlide command)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToPage("./Create", command);
            }
            var result = _slideApplication.Create(command);
            return new JsonResult(result);
        }
        public IActionResult OnGetEdit(long id)
        {
            var command = _slideApplication.GetDetails(id);

            return Partial("./Edit", command);
        }
        public IActionResult OnPostEdit(EditSlide command)
        {
            var resul = _slideApplication.Edit(command);
            return new JsonResult(resul);
        }
        public IActionResult OnGetRemove(long id)
        {
            _slideApplication.Remove(id);
            return RedirectToPage("./Index");
        }
        public IActionResult OnGetRestore(long id)
        {
            _slideApplication.Restore(id);
            return RedirectToPage("./Index");
        }
    }
}
