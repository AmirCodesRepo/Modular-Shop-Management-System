using _0_Framework.Infrastucture;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.ProductPictureAgg;
using System.Linq;

namespace ShopManagement.Infrastructure.EFCore.Repository
{
    public class ProductPictureRepostirory : RepositoryBase<long,ProductPicture>,IProductPictureRepository

    {
        private readonly ShopContext _context;
        public ProductPictureRepostirory(ShopContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<ProductPicture> GetProductPictures(long id)
        {
            var productPictures = _context.productPictures.Where(x => x.ProductId == id).AsQueryable();
            return productPictures;
        }

        public ProductPicture GetWithProductAndCategory(long Id)
        {
            return _context.productPictures.Include(x => x.Product).ThenInclude(x => x.Category).FirstOrDefault(x => x.Id == Id);
        }
    }
}
