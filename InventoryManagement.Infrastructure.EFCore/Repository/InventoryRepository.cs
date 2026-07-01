using _0_Framework.Infrastucture;
using InventoryManagement.Domain.InventoryAgg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Infrastructure.EFCore.Repository
{
    public class InventoryRepository : RepositoryBase<long, Inventory>, IInventoryRepository
    {
        private readonly InventoryContext _context;
        public InventoryRepository(InventoryContext context) : base(context)
        {
            _context = context;
        }
        public Inventory GetByProductId(long productId)
        {
            return _context.inventory.FirstOrDefault(x => x.ProductId == productId);
        }

        public List<Inventory> Search(long productId, bool inStock)
        {
            var query = _context.inventory.AsQueryable();

            if(productId > 0)
                query = query.Where(x => x.ProductId == productId);

            if (inStock)
                query = query.Where(x => !x.InStock);

            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}
