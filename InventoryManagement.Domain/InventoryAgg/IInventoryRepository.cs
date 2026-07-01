using _0_Framework.Domain;
using System.Collections.Generic;

namespace InventoryManagement.Domain.InventoryAgg
{
    public interface IInventoryRepository : IRepository<long,Inventory>
    {
        Inventory GetByProductId(long productId);
        List<Inventory> Search(long productId, bool inStock);
    }
}
