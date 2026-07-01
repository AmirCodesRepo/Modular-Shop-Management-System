using _0_Framework.Domain;
using System.Collections.Generic;

namespace ShopManagement.Domain.ProductAgg
{
    public interface IProductRepository : IRepository<long,Product>
    {
        Dictionary<long,string> GetProductsName(List<long> ids);
        List<LookupItemDto> GetNames();

        Product GetProductWithCategory(long Id);
        List<Product> Search(string name, string code, long categoryId,int inStock);
    }
}
