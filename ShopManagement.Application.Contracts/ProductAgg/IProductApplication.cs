using _0_Framework.Application;
using _0_Framework.Domain;
using System.Collections.Generic;

namespace ShopManagement.Application.Contracts.ProductAgg
{
    public interface IProductApplication
    {
        OperationResult Create(CreateProduct command);
        OperationResult Edit(EditProduct command);
        OperationResult Instok(long id);
        OperationResult NotInstok(long id);
        EditProduct GetDetails(long id);
        List<ProductViewModel> GetProducts();
        List<ProductViewModel> Search(ProductSearchModel searchModel);

        Dictionary<long, string> GetProductsName(List<long> ids);
        List<LookupItemDto> GetNames();
    }
}
