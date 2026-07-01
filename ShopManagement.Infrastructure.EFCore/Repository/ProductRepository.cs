using ShopManagement.Domain.ProductAgg;
using _0_Framework.Infrastucture;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using _0_Framework.Domain;

namespace ShopManagement.Infrastructure.EFCore.Repository
{
    public class ProductRepository : RepositoryBase<long, Product>, IProductRepository
    {
        private readonly ShopContext _context;
        public ProductRepository(ShopContext context) : base(context)
        {
            _context = context;
        }

        public Dictionary<long, string> GetProductsName(List<long> ids)
        {
            return _context.Products.Where(p => ids.Contains(p.Id)).ToDictionary(p => p.Id, p => p.Name);
        }
        public List<LookupItemDto> GetNames()
        {
            return _context.Products.Select( x => new LookupItemDto { ProductId = x.Id ,ProductName = x.Name}).ToList();
        }

        public List<Product> Search(string name, string code, long categoryId, int instock)
        {
            var query = _context.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(P => P.Name.Contains(name));
            if (!string.IsNullOrWhiteSpace(code))
                query = query.Where(p => p.Code.Contains(code));
            if (categoryId != 0)
                query = query.Where(p => p.CategoryId == categoryId);
            if (instock == 1 || instock == 2)
            {
                bool res = instock == 1 ? true : false;
                query = query.Where(p => p.IsInStock == res);
            }

            return query
                .Include(x => x.Category)
                .OrderByDescending(x => x.Id).ToList();
        }

        public Product GetProductWithCategory(long Id)
        {
            return _context.Products.Include(x => x.Category).FirstOrDefault(x => x.Id == Id);
        }

    }
}
