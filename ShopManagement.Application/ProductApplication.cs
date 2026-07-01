using _0_Framework.Application;
using _0_Framework.Domain;
using ShopManagement.Application.Contracts.ProductAgg;
using ShopManagement.Application.Mapping;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductCategoryAgg;
using System.Collections.Generic;
using System.Linq;

namespace ShopManagement.Application
{
    public class ProductApplication : IProductApplication
    {
        private readonly IProductRepository _productRepository;
        public readonly IFileUploader _fileUploader;
        public readonly IProductCategoryRepository _productCategoryRepository;
            
        
        public ProductApplication(IProductRepository productRepository, IFileUploader fileUploader,IProductCategoryRepository productCategoryRepository)
        {
            _fileUploader = fileUploader;
            _productRepository = productRepository;
            _productCategoryRepository = productCategoryRepository;
        }
        public OperationResult Create(CreateProduct command)
        {
            var operation = new OperationResult();
            if (_productRepository.Exists(itme => itme.Name == command.Name))
                return operation.Failed(OperationMessages.DuplicateItme);

            var slug = command.Slug.Slugify();
            
            var categorySlug = _productCategoryRepository.GetSlugById(command.CategoryId);
            
            var path = $"{categorySlug}/{slug}";
            
            var picturePath = _fileUploader.UploadFile(command.Picture,path);

            _productRepository.Create(MapTo.NewProduct(command,picturePath));
            _productRepository.SaveChanges();

            return operation.Succeded();
        }

        public OperationResult Edit(EditProduct command)
        {
            var operation = new OperationResult();
            var product = _productRepository.GetProductWithCategory(command.Id);

            if (product == null)
                return operation.Failed(OperationMessages.NotFoundItem);

            if (_productRepository.Exists(item => item.Name == command.Name && item.Id != command.Id))
                return operation.Failed(OperationMessages.DuplicateItme);

            var slug = command.Slug.Slugify();

            var path = $"{product.Category.Slug}/{slug}";

            var picturePath = _fileUploader.UploadFile(command.Picture, path);
            
            MapTo.ExistsProduct(product, command , picturePath);
            _productRepository.SaveChanges();
            return operation.Succeded();
        }

        public EditProduct GetDetails(long id)
        {
            var product = _productRepository.Get(id);
            return MapTo.EditProduct(product);
        }

        public List<LookupItemDto> GetNames()
        {
            return _productRepository.GetNames();
        }

        public List<ProductViewModel> GetProducts()
        {
            return _productRepository.GetNames().Select(x => new ProductViewModel
            {
                Name = x.ProductName,
                Id = x.ProductId
            }).ToList();
        }

        public Dictionary<long, string> GetProductsName(List<long> ids)
        {
            return _productRepository.GetProductsName(ids);
        }

        public OperationResult Instok(long id)
        {
            var operation = new OperationResult();
            var item = _productRepository.Get(id);
            if (item == null)
                return operation.Failed(OperationMessages.NotFoundItem);

            item.InStock();

            _productRepository.SaveChanges();
            return operation.Succeded();
        }

        public OperationResult NotInstok(long id)
        {
            var operation = new OperationResult();
            var item = _productRepository.Get(id);
            if (item == null)
                return operation.Failed(OperationMessages.NotFoundItem);

            item.NotInStock();

            _productRepository.SaveChanges();
            return operation.Succeded();
        }

        public List<ProductViewModel> Search(ProductSearchModel searchModel)
        {
            var products = _productRepository.Search(searchModel.Name, searchModel.Code, searchModel.CategoryId, searchModel.InStock);
            
            var res = products.Select(p => new ProductViewModel
            {
                Id = p.Id,
                Name = p.Name,
                Code = p.Code,
                Picture = p.Picture,
                IsInStock = p.IsInStock,
                Category = p.Category.Name,
                CreationDate = p.CreationDate.ToFarsi(),
            });

            return res.ToList();
        }
    }
}
