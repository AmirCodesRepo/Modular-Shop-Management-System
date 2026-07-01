using _0_Framework.Application;
using ShopManagement.Application.Contracts.ProductAgg;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DiscountManagement.Application.Contracts.CustomerDiscountAgg
{
    public class DefineCustomerDiscount
    {
        [Range(1, long.MaxValue, ErrorMessage = ValidationMessages.IsRequired)]
        public long ProductId { get; set; }

        [Range(1, 99, ErrorMessage = ValidationMessages.InvalidDiscount)]
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public int DiscountRate { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string StartDate { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string EndDate { get; set; }

        public string Reason { get; set; }
        public List<ProductViewModel> Proudcts { get; set; }
    }
}
