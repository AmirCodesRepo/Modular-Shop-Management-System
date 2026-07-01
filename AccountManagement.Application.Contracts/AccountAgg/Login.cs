using _0_Framework.Application;
using System.ComponentModel.DataAnnotations;

namespace AccountManagement.Application.Contracts.AccountAgg
{
    public class Login
    {
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string UserName { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Password { get; set; }
    }
}
