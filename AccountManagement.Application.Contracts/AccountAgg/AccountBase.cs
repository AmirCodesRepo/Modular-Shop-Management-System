using _0_Framework.Application;
using AccountManagement.Application.Contracts.RoleAgg;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AccountManagement.Application.Contracts.AccountAgg
{
    public class AccountBase
    {
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string FullName { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string UserName { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Mobile { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [Range(1, 100, ErrorMessage = ValidationMessages.InvalidInput)]
        public long RoleId { get; set; }

        public string ProfilePhoto { get; set; }

        public List<RoleViewModel> Roles { get; set; }
    }
}
