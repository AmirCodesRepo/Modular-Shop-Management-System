using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using ShopManagement.Application.Contracts.ProductCategoryAgg;
using ShopManagement.Application.Mapping;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Application
{
    public class ProductCategoryApplication : IProductCategoryApplication
    {
        private readonly IProductCategoryRepository _productCategoryRepository;
        public readonly IFileUploader _fileUploader;

        public ProductCategoryApplication(IProductCategoryRepository productCategoryRepository, IFileUploader fileUploader)
        {
            _productCategoryRepository = productCategoryRepository;
            _fileUploader = fileUploader;
        }
        public OperationResult Create(CreateProductCategory command)
        {
            var operation = new OperationResult();

            if (_productCategoryRepository.Exists(x => x.Name == command.Name))
                return operation.Failed(OperationMessages.DuplicateItme);

            var path = $"{command.Slug}";
            var file = _fileUploader.UploadFile(command.Picture,path);

            _productCategoryRepository.Create(MapTo.NewProductCategory(command,file));
            _productCategoryRepository.SaveChanges();

            return operation.Succeded();
        }

        public OperationResult Edit(EditProductCategory command)
        {
            var operation = new OperationResult();
            var productCategory = _productCategoryRepository.Get(command.Id);

            if (productCategory == null)
                return operation.Failed(OperationMessages.NotFoundItem);

            if (_productCategoryRepository.Exists(x => x.Name == command.Name && x.Id != command.Id))
                return operation.Failed(OperationMessages.DuplicateItme);
            var path = $"{command.Slug}";
            var fileName = _fileUploader.UploadFile(command.Picture, path);

            MapTo.ExistsProductCategory(productCategory, command ,fileName);
            _productCategoryRepository.SaveChanges();
            return operation.Succeded();
        }

        public EditProductCategory GetDetails(long id)
        {
            var category = _productCategoryRepository.Get(id);

           return MapTo.EditProductCategory(category);
        }

        public List<ProductCategoryViewModel> GetProductCategories()
        {
            return _productCategoryRepository.GetCategories().Select(x => new ProductCategoryViewModel
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
        }

        public List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel)
        {
            string productCategoryName = searchModel.Name ?? "";

            var productCategories = _productCategoryRepository.Search(productCategoryName);

            return productCategories.Select(p => new ProductCategoryViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Picture = p.Picture,
                CreationDate = p.CreationDate.ToFarsi(),
                ProductsCount = 0

            }).ToList();
        }
    }
}
