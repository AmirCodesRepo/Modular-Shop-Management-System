using _0_Framework.Application;
using ShopManagement.Application.Contracts.ProductPictureAgg;
using ShopManagement.Application.Mapping;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductPictureAgg;
using System.Collections.Generic;
using System.Linq;

namespace ShopManagement.Application
{
    public class ProductPictureApplication : IProductPictureApplication
    {
        private readonly IProductPictureRepository _productPictureRepository;
        private readonly IFileUploader _fileUploader;
        private readonly IProductRepository _productRepository;
        public ProductPictureApplication(IProductPictureRepository productPictureRepository,
                                         IFileUploader fileUploader,
                                         IProductRepository productRepository)
        {
            _productPictureRepository = productPictureRepository;
            _fileUploader = fileUploader;
            _productRepository = productRepository;
        }
        public OperationResult Create(CreateProductPicture command)
        {
            var operation = new OperationResult();

            var product = _productRepository.GetProductWithCategory(command.ProductId);
            
            var path = $"{product.Category.Slug}/{product.Slug}";

            var picturePath = _fileUploader.UploadFile(command.Picture, path);

            _productPictureRepository.Create(MapTo.NewProductPicture(command, picturePath));
            _productPictureRepository.SaveChanges();

            return operation.Succeded();
        }

        public OperationResult Edit(EditProductPicture command)
        {
            var opertion = new OperationResult();
            var productPicture = _productPictureRepository.GetWithProductAndCategory(command.Id);

            if (productPicture == null)
                return opertion.Failed(OperationMessages.NotFoundItem);

            var path = $"{productPicture.Product.Category.Slug}/{productPicture.Product.Slug}";

            var picturePath = _fileUploader.UploadFile(command.Picture, path);

            MapTo.ExistsProductPicture(productPicture, command, picturePath);
            _productPictureRepository.SaveChanges();

            return opertion.Succeded();
        }

        public EditProductPicture GetDetails(long id)
        {
            var productPicture = _productPictureRepository.Get(id);
            return MapTo.EditProductPicture(productPicture);
        }

        public List<ProductPictureViewModel> GetProductPictures(long id)
        {
            var productPictures = _productPictureRepository.GetProductPictures(id);

            return productPictures.Select(x => new ProductPictureViewModel
            {
                Id = x.Id,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Product = x.Product.Name,
                CreationDate = x.CreationDate.Date.ToString(),
                IsRemoved = x.IsRemoved,
            }).ToList();
        }

        public OperationResult Remove(long id)
        {
            var opertion = new OperationResult();
            var productPicture = _productPictureRepository.Get(id);

            if (productPicture == null)
                return opertion.Failed(OperationMessages.NotFoundItem);

            productPicture.Remove();
            _productPictureRepository.SaveChanges();

            return opertion.Succeded();
        }

        public OperationResult Restore(long id)
        {
            var opertion = new OperationResult();
            var productPicture = _productPictureRepository.Get(id);

            if (productPicture == null)
                return opertion.Failed(OperationMessages.NotFoundItem);

            productPicture.Restore();
            _productPictureRepository.SaveChanges();

            return opertion.Succeded();
        }
    }
}
