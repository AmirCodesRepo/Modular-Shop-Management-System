using _0_Framework.Infrastucture;
using AccountManagement.Application.Contracts.RoleAgg;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace ServiceHost.Areas.Administration.Pages.Users.Roles
{
    [Authorize(Roles = MyRoles.Administrator)]
    public class IndexModel : PageModel
    {
        public List<RoleViewModel> Roles { get; set; }

        private readonly IRoleApplication _roleApplication;
        public IndexModel(IRoleApplication roleApplication)
        {
            _roleApplication = roleApplication;
        }

        public void OnGet()
        {
            Roles = _roleApplication.List();
        }

        public IActionResult OnGetCreate()
        {
            var command = new CreateRole()
            {
                
            };
            return Partial("./Create", command);
        }

        public IActionResult OnPostCreate(CreateRole command)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToPage("./Create", command);
            }
            var result = _roleApplication.Create(command);
            return new JsonResult(result);
        }
        public IActionResult OnGetEdit(long id)
        {
            var command = _roleApplication.GetDetailes(id);

            return Partial("./Edit", command);
        }

        public IActionResult OnPostEdit(EditRole command)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToPage("./Edit", command);
            }
            var resul = _roleApplication.Edit(command);

            return new JsonResult(resul);
        }
    }
}
