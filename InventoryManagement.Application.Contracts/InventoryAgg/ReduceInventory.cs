using _0_Framework.Application;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Application.Contracts.InventoryAgg
{
    public class ReduceInventory
    {
        public long InventoryId { get; set; }
        public long ProductId { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [Range(1, double.MaxValue, ErrorMessage = ValidationMessages.InvalidInput)]
        public long Count { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Description { get; set; }
        public long OrderId { get; set; }
    }
}
