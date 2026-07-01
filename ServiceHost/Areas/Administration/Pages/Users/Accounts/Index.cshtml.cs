using _0_Framework.Infrastucture;
using AccountManagement.Application.Contracts.AccountAgg;
using AccountManagement.Application.Contracts.RoleAgg;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace ServiceHost.Areas.Administration.Pages.Users.Accounts
{
    [Authorize(Roles = MyRoles.Administrator)]
    public class IndexModel : PageModel
    {
        public List<AccountViewModel> Accounts { get; set; }
        public AccountSearchModel SearchModel { get; set; }
        public SelectList Roles { get; set; }

        private readonly IAccountApplication _accountApplication;
        private readonly IRoleApplication _roleApplication;
        public IndexModel(IAccountApplication accountApplication,IRoleApplication roleApplication)
        {
            _accountApplication = accountApplication;
            _roleApplication = roleApplication;
        }

        public void OnGet(AccountSearchModel searchModel)
        {
            Roles = new SelectList(_roleApplication.List() , "Id" , "Name");
            Accounts = _accountApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            var command = new RegisterAccount()
            {
                Roles = _roleApplication.List()
            };
            return Partial("./Create", command);
        }

        public IActionResult OnPostCreate(RegisterAccount command)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToPage("./Create", command);
            }
            var result = _accountApplication.Register(command);
            return new JsonResult(result);
        }
        public IActionResult OnGetEdit(long id)
        {
            var command = _accountApplication.GetDetailes(id);
            
            command.Roles = _roleApplication.List();

            return Partial("./Edit", command);
        }

        public IActionResult OnPostEdit(EditAccount command)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToPage("./Edit", command);
            }
            var resul = _accountApplication.Edit(command);

            return new JsonResult(resul);
        }
        public IActionResult OnGetChangePassword(long id)
        {
            var command = new ChangePassword()
            {
                Id = id
            };
            return Partial("./ChangePassword",command);
        }

        public IActionResult OnPostChangePassword(ChangePassword command)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToPage("./ChangePassword", command);
            }
            var resul = _accountApplication.ChangePassword(command);
            return new JsonResult(resul);
        }
    }
}
