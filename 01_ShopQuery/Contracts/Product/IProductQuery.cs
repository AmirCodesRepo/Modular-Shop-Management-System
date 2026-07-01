using System.Collections.Generic;

namespace _01_ShopQuery.Contracts.Product
{
    public interface IProductQuery
    {
        ProductQueryModel GetDetailes(string slug);
        List<ProductQueryModel> GetLatestArrivals();
        List<ProductQueryModel> Search(string value);
    }
}
