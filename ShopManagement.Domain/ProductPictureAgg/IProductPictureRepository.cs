using _0_Framework.Domain;
using System.Linq;

namespace ShopManagement.Domain.ProductPictureAgg
{
    public interface IProductPictureRepository : IRepository<long , ProductPicture>
    {
        IQueryable<ProductPicture> GetProductPictures(long id);
        ProductPicture GetWithProductAndCategory(long Id);
    }
}
