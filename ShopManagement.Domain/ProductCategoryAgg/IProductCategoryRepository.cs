using _0_Framework.Domain;
using System.Linq;

namespace ShopManagement.Domain.ProductCategoryAgg
{
    public interface IProductCategoryRepository : IRepository<long,ProductCategory>
    {
        IQueryable<ProductCategory> GetCategories();
        IQueryable<ProductCategory> Search(string name);
        string GetSlugById(long id);
         }
}
