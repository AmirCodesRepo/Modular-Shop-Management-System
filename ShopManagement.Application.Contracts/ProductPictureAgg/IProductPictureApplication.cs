using _0_Framework.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManagement.Application.Contracts.ProductPictureAgg
{
    public interface IProductPictureApplication
    {
        OperationResult Create(CreateProductPicture command);
        OperationResult Edit(EditProductPicture command);
        EditProductPicture GetDetails(long id);

        OperationResult Remove(long id);
        OperationResult Restore(long id);
        List<ProductPictureViewModel> GetProductPictures(long id);
    }
}
