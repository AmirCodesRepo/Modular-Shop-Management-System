using _0_Framework.Application;
using System.ComponentModel.DataAnnotations;

namespace AccountManagement.Application.Contracts.AccountAgg
{
    public class ChangePassword
    {
        public long Id { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Password { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [Compare("Password",ErrorMessage = ValidationMessages.PasswordNotMatch)]
        public string RePassword { get; set; }
    }
}