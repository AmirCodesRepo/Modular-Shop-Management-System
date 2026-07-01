using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.Contracts.CommentAgg;
using System.Collections.Generic;

namespace ServiceHost.Areas.Administration.Pages.Shop.Comments
{
    public class IndexModel : PageModel
    {
        public List<CommentViewModel> Comments { get; set; }
        public CommentSearchModle SearchModel { get; set; }

        private readonly ICommentApplication _commentApplication;

        public IndexModel(ICommentApplication commentApplication)
        {
            _commentApplication = commentApplication;
        }

        public void OnGet(CommentSearchModle searchModel)
        {
            Comments = _commentApplication.Search(searchModel);
        }
        public IActionResult OnGetCancel(long id)
        {
            _commentApplication.Cancel(id);
            return RedirectToPage("./Index");
        }
        public IActionResult OnGetConfirm(long id)
        {
            _commentApplication.Confirm(id);
            return RedirectToPage("./Index");
        }
    }
}
