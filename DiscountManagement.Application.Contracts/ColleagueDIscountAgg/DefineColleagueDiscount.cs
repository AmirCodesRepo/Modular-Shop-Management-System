using _0_Framework.Application;
using Microsoft.VisualBasic;
using ShopManagement.Application.Contracts.ProductAgg;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DiscountManagement.Application.Contracts.ColleagueDiscountAgg
{
    public class DefineColleagueDiscount
    {
        [Range(1, long.MaxValue, ErrorMessage = ValidationMessages.IsRequired)]
        public long ProductId { get; set; }

        [Range(1, 99, ErrorMessage = ValidationMessages.InvalidDiscount)]
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public int DiscountRate { get; set; }

        public List<ProductViewModel> Products { get; set; }
    }
}
