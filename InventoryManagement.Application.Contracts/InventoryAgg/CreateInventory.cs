using _0_Framework.Application;
using ShopManagement.Application.Contracts.ProductAgg;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Application.Contracts.InventoryAgg
{
    public class CreateInventory
    {
        [Range(1, long.MaxValue, ErrorMessage = ValidationMessages.IsRequired)]
        public long ProductId { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [Range(1000, double.MaxValue, ErrorMessage = ValidationMessages.InvalidInput)]
        public double UnitPrice { get; set; }

        public List<ProductViewModel> Products { get; set; }
    }
}
