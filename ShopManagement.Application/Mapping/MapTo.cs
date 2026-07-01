using _0_Framework.Application;
using ShopManagement.Application.Contracts.ProductAgg;
using ShopManagement.Application.Contracts.ProductCategoryAgg;
using ShopManagement.Application.Contracts.ProductPictureAgg;
using ShopManagement.Application.Contracts.SlideAgg;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductCategoryAgg;
using ShopManagement.Domain.ProductPictureAgg;
using ShopManagement.Domain.SlideAgg;

namespace ShopManagement.Application.Mapping
{
    public class MapTo
    {
        public readonly IFileUploader _fileUploader;
        public MapTo(IFileUploader fileUploader)
        {
            _fileUploader = fileUploader;
        }


        #region Product
        public static Product NewProduct(CreateProduct command, string picturePath)
        {
            var slug = command.Slug.Slugify();

            var result = new Product(command.Name, command.Code,
                command.ShortDescription, command.Description, picturePath, command.PictureAlt,
                command.PictureTitle, command.Keywords, command.MetaDescription, slug, command.CategoryId);

            return result;
        }
        public static void ExistsProduct(Product product, EditProduct command, string picturePath)
        {
            var slug = command.Slug.Slugify();

            product.Edit(command.Name, command.Code, command.ShortDescription,
                command.Description, picturePath, command.PictureAlt, command.PictureTitle,
                command.Keywords, command.MetaDescription, slug, command.CategoryId);
        }
        public static EditProduct EditProduct(Product product)
        {
            return new EditProduct
            {
                Id = product.Id,
                Name = product.Name,
                Code = product.Code,
                IsInStock = product.IsInStock,
                ShortDescription = product.ShortDescription,
                Description = product.Description,
                PictureAlt = product.PictureAlt,
                PictureTitle = product.PictureTitle,
                Keywords = product.Keywords,
                MetaDescription = product.MetaDescription,
                Slug = product.Slug.Slugify(),
                CategoryId = product.CategoryId
            };
        }
        #endregion

        #region ProductCategory
        public static ProductCategory NewProductCategory(CreateProductCategory command, string fileName)
        {
            var slug = command.Slug.Slugify();
            var productCategory = new ProductCategory(command.Name, command.Description,
                fileName, command.PictureAlt, command.PictureTitle, command.Keywords,
                command.MetaDescription, slug);

            return productCategory;
        }
        public static void ExistsProductCategory(ProductCategory productCategory, EditProductCategory command, string fileName)
        {
            var slug = command.Slug.Slugify();

            productCategory.Edit(command.Name, command.Description,
                fileName, command.PictureAlt, command.PictureTitle,
                command.Keywords, command.MetaDescription, slug);
        }
        public static EditProductCategory EditProductCategory(ProductCategory productCategory)
        {
            return new EditProductCategory
            {
                Id = productCategory.Id,
                Name = productCategory.Name,
                Description = productCategory.Description,
                PictureAlt = productCategory.PictureAlt,
                PictureTitle = productCategory.PictureTitle,
                Keywords = productCategory.Keywords,
                MetaDescription = productCategory.MetaDescription,
                Slug = productCategory.Slug.Slugify(),
            };
        }
        #endregion

        #region ProductPicture
        public static ProductPicture NewProductPicture(CreateProductPicture command, string picturePath)
        {
            var productPicture = new ProductPicture(command.ProductId, picturePath, command.PictureAlt,
                command.PictureTitle);

            return productPicture;
        }
        public static void ExistsProductPicture(ProductPicture productPicture, EditProductPicture command, string picturePath)
        {
            productPicture.Edit(command.ProductId, picturePath, command.PictureAlt, command.PictureTitle);
        }

        public static EditProductPicture EditProductPicture(ProductPicture productPicture)
        {
            return new EditProductPicture
            {
                Id = productPicture.Id,
                PictureAlt = productPicture.PictureAlt,
                PictureTitle = productPicture.PictureTitle,
                ProductId = productPicture.ProductId
            };
        }
        #endregion

        #region Slide
        public static Slide NewSlide(CreateSlide command, string picturePath)
        {
            var slide = new Slide(picturePath, command.PictureAlt, command.PictureTitle,
                command.Heading, command.Title, command.Text, command.BtnText, command.Link);
            return slide;
        }
        public static void ExistsSlide(Slide slide, EditSlide command, string picturePath)
        {
            slide.Edit(picturePath, command.PictureAlt, command.PictureTitle,
                command.Heading, command.Title, command.Text, command.BtnText, command.Link);
        }

        public static EditSlide EditSlide(Slide slide)
        {
            return new EditSlide
            {
                Id = slide.Id,
                PictureAlt = slide.PictureAlt,
                PictureTitle = slide.PictureTitle,
                Heading = slide.Heading,
                Title = slide.Title,
                Text = slide.Text,
                BtnText = slide.BtnText,
                Link = slide.Link,
            };
        }
        #endregion
    }
}
