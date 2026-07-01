using _0_Framework.Application;
using System.ComponentModel.DataAnnotations;

namespace InventoryManagement.Application.Contracts.InventoryAgg
{
    public class IncreaseInventory
    {
        public long OperatorId { get; set; }
        public long InventoryId { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        [Range(1, double.MaxValue, ErrorMessage = ValidationMessages.InvalidInput)]
        public long Count { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Description { get; set; }
    }
}
