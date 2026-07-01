using _0_Framework.Application;
using _01_ShopQuery.Contracts.Product;
using DiscountManagement.Infrastructure.EFCore;
using InventoryManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.CommentAgg;
using ShopManagement.Domain.ProductPictureAgg;
using ShopManagement.Infrastructure.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _01_ShopQuery.Query
{
    public class ProductQuery : IProductQuery
    {
        private readonly ShopContext _shopContext;
        private readonly DiscountContext _discountContext;
        private readonly InventoryContext _inventoryContext;
        public ProductQuery(ShopContext shopContext, DiscountContext discountContext
            , InventoryContext inventoryContext)
        {
            _shopContext = shopContext;
            _discountContext = discountContext;
            _inventoryContext = inventoryContext;
        }

        public ProductQueryModel GetDetailes(string slug)
        {
            var inventory = _inventoryContext.inventory
                .Select(x => new { x.ProductId, x.UnitPrice, x.InStock })
                .ToList();

            var discounts = _discountContext.customerDiscounts
                .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .Select(x => new { x.ProductId, x.DiscountRate, x.EndDate })
                .ToList();

            var product = _shopContext.Products
                .Include(x => x.Category)
                .Include(x => x.ProductPictures)
                .Include(x => x.Comments)
                .Select(product => new ProductQueryModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    Category = product.Category.Name,
                    Picture = product.Picture,
                    PictureAlt = product.PictureAlt,
                    PictureTitle = product.PictureTitle,
                    ProductPictures = MapToProductPicture(product.ProductPictures),
                    ProeuctComments = MapToProductComment(product.Comments),
                    Slug = product.Slug,
                    CategorySlug = product.Category.Slug,
                    Code = product.Code,
                    Description = product.Description,
                    Keywords = product.Keywords,
                    MetaDescription = product.MetaDescription,
                    shortDescription = product.ShortDescription,
                    IsNew = product.CreationDate.AddDays(3) > DateTime.Now ? true : false

                }).FirstOrDefault(x => x.Slug == slug);

            if (product == null)
                return new ProductQueryModel();

            var productInventory = inventory.FirstOrDefault(x => x.ProductId == product.Id);

            if (productInventory != null)
            {
                var price = productInventory.UnitPrice;
                product.Price = price.ToMoney();

                product.IsInStock = productInventory.InStock;

                var discount = discounts.FirstOrDefault(x => x.ProductId == product.Id);
                if (discount != null)
                {
                    int discountRate = discount.DiscountRate;
                    product.DiscountRate = discountRate;
                    product.HasDiscount = discountRate > 0;
                    var discountAmount = Math.Round((discountRate * price) / 100);

                    product.PriceWhitDiscount = (price - discountAmount).ToMoney();
                    product.DiscountExpireDate = discount.EndDate.ToDiscountFormat();
                }
            }

            return product;
        }

        private static List<ProeuctCommentQueryModel> MapToProductComment(List<Comment> comments)
        {
            return comments
                .Where(x => !x.IsCanceld)
                .Where(x => x.IsConfirmed)
                .Select(x => new ProeuctCommentQueryModel
                {
                    Id = x.Id,
                    Email = x.Email,
                    Message = x.Message,
                    Name = x.Name

                }).OrderByDescending(x=> x.Id).ToList();
        }

        private static List<ProductPictureQueryModel> MapToProductPicture(List<ProductPicture> productPictures)
        {
            return productPictures.Select(x => new ProductPictureQueryModel
            {
                IsRemoved = x.IsRemoved,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                ProductId = x.ProductId,
            }).Where(x => x.IsRemoved == false).ToList();
        }

        public List<ProductQueryModel> GetLatestArrivals()
        {
            var inventory = _inventoryContext.inventory
                .Select(x => new { x.ProductId, x.UnitPrice })
                .ToList();

            var discounts = _discountContext.customerDiscounts
                .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .Select(x => new { x.ProductId, x.DiscountRate, x.EndDate })
                .ToList();

            var products = _shopContext.Products
                .Include(x => x.Category)
                .Select(product => new ProductQueryModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    Category = product.Category.Name,
                    Picture = product.Picture,
                    PictureAlt = product.PictureAlt,
                    PictureTitle = product.PictureTitle,
                    Slug = product.Slug,
                    IsNew = product.CreationDate.AddDays(3) > DateTime.Now ? true : false,
                    Price = "",
                    PriceWhitDiscount = ""

                }).OrderByDescending(x => x.Id).Take(6).ToList();

            foreach (var product in products)
            {
                var productInventory = inventory.FirstOrDefault(x => x.ProductId == product.Id);

                if (productInventory != null)
                {
                    var price = productInventory.UnitPrice;
                    product.Price = price.ToMoney();

                    var discount = discounts.FirstOrDefault(x => x.ProductId == product.Id);
                    if (discount != null)
                    {
                        int discountRate = discount.DiscountRate;
                        product.DiscountRate = discountRate;
                        product.HasDiscount = discountRate > 0;
                        var discountAmount = Math.Round((discountRate * price) / 100);
                        var priceWithDiscount = price - discountAmount;
                        product.PriceWhitDiscount = priceWithDiscount.ToMoney();
                    }
                }

            }

            return products;
        }

        public List<ProductQueryModel> Search(string value)
        {
            var inventory = _inventoryContext.inventory
                .Select(x => new { x.ProductId, x.UnitPrice })
                .ToList();

            var discount = _discountContext.customerDiscounts
                .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .Select(x => new { x.ProductId, x.DiscountRate, x.EndDate })
                .ToList();

            var query = _shopContext.Products
                .Include(x => x.Category)
                .Select(product => new ProductQueryModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    Category = product.Category.Name,
                    CategorySlug = product.Category.Slug,
                    Picture = product.Picture,
                    PictureAlt = product.PictureAlt,
                    PictureTitle = product.PictureTitle,
                    Slug = product.Slug,
                    shortDescription = product.ShortDescription,
                    IsNew = product.CreationDate.AddDays(3) > DateTime.Now ? true : false,
                }).AsNoTracking();


            if (!string.IsNullOrWhiteSpace(value))
                query = query.Where(x => x.Name.Contains(value) || x.shortDescription.Contains(value));

            var Products = query.OrderByDescending(x => x.Id).ToList();

            foreach (var product in Products)
            {
                var price = inventory.FirstOrDefault(x => x.ProductId == product.Id).UnitPrice;
                product.Price = price.ToMoney();

                int discountRate = discount?.FirstOrDefault(x => x.ProductId == product.Id)?.DiscountRate ?? 0;

                product.DiscountRate = discountRate;

                var discountAmount = Math.Round((price * discountRate) / 100);

                product.PriceWhitDiscount = (price - discountAmount).ToMoney();

                product.HasDiscount = discountRate > 0;

                product.DiscountExpireDate = discount?.FirstOrDefault(x => x.ProductId == product.Id)?.EndDate.ToDiscountFormat();

            }
            return Products;
        }
    }
}
