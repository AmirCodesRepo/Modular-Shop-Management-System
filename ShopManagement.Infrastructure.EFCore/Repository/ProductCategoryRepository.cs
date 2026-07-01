using _0_Framework.Infrastucture;
using ShopManagement.Domain.ProductCategoryAgg;
using System.Collections.Generic;
using System.Linq;

namespace ShopManagement.Infrastructure.EFCore.Repository
{
    public class ProductCategoryRepository : RepositoryBase<long, ProductCategory>, IProductCategoryRepository
    {
        private readonly ShopContext _context;
        public ProductCategoryRepository(ShopContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<ProductCategory> GetCategories()
        {
            return _context.ProductCategories;
        }

        public string GetSlugById(long id)
        {
            return _context.ProductCategories.Select(x => new {x.Id , x.Slug}).FirstOrDefault(x => x.Id == id).Slug;
        }

        public IQueryable<ProductCategory> Search(string name)
        {
            var query = _context.ProductCategories.AsQueryable();

            if (!string.IsNullOrEmpty(name))
                query = query.Where(x => x.Name.Contains(name));

            return query.OrderByDescending(x => x.Id);
        }
    }
}
