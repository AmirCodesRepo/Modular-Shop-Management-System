using AccountManagement.Application.Contracts.AccountAgg;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;

namespace ServiceHost.Pages
{
    public class AccountModel : PageModel
    {
        [BindProperty]
        public RegisterAccount register {  get; set; }

        [BindProperty]
        public Login login { get; set; }

        [TempData]
        public string LoginMessage { get; set; }

        [TempData]
        public string SigninMessage { get; set; }

        private readonly IAccountApplication _accountApplication;
        public AccountModel(IAccountApplication accountApplication)
        {
            _accountApplication = accountApplication;
        }

        public void OnGet()
        {

        }

        public IActionResult OnPostLogin()
        {
            
            var result = _accountApplication.LoginUser(login);
            if (result.IsSucceded)
            {
                return RedirectToPage("/Index");
            }

            //TempData["test"] = "test";
            //TempData["Message"] = result.Message;
            TempData["Message"] = "Message is here";

            LoginMessage = result.Message;
            //TempData.Keep("Message");
            return Page();
            //return Redirect(".");
            //return LocalRedirect("/Account");
            //return RedirectToPage("/Account");
        }

        public IActionResult OnGetSignout()
        {
            _accountApplication.Signout();

            return RedirectToPage("/Index");
        }

        public IActionResult OnPostSignin()
        {
            
            var result = _accountApplication.Register(register);

            if (result.IsSucceded)
                return RedirectToPage("/Account");

            SigninMessage = result.Message;

            return Page();
        }
    }
}
