using _0_Framework.Application;
using _01_ShopQuery.Contracts.Product;
using _01_ShopQuery.Contracts.ProductCategory;
using DiscountManagement.Infrastructure.EFCore;
using InventoryManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Infrastructure.EFCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _01_ShopQuery.Query
{
    public class ProductCategoryQuery : IProductCategoryQuery
    {
        private readonly ShopContext _shopContext;
        private readonly InventoryContext _inventoryContext;
        private readonly DiscountContext _discountContext;
        public ProductCategoryQuery(ShopContext shopContext, InventoryContext inventoryContext, DiscountContext discountContext)
        {
            _shopContext = shopContext;
            _inventoryContext = inventoryContext;
            _discountContext = discountContext;
        }
        public List<ProductCategoryQueryModel> GetProductCategories()
        {
            return _shopContext.ProductCategories.Select(x => new ProductCategoryQueryModel
            {
                Name = x.Name,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Slug = x.Slug

            }).AsNoTracking().Take(4).ToList();
        }

        public List<ProductCategoryQueryModel> GetProductCategoriesForMenu()
        {
            return _shopContext.ProductCategories.Select(x => new ProductCategoryQueryModel
            {
                Name = x.Name,
                Slug = x.Slug

            }).AsNoTracking().ToList();
        }
        public List<ProductCategoryQueryModel> GetProductCategoryWithProducts()
        {
            var inventory = _inventoryContext.inventory
                .Select(product => new { product.ProductId, product.UnitPrice })
                .ToList();

            var discounts = _discountContext.customerDiscounts
                .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .Select(x => new { x.ProductId, x.DiscountRate }).ToList();

            var Categories = _shopContext.ProductCategories
                 .Include(x => x.Products)
                 .ThenInclude(x => x.Category)
                 .Select(x => new ProductCategoryQueryModel
                 {
                     Id = x.Id,
                     Name = x.Name,
                     Products = MapProduct(x.Products),


                 }).AsNoTracking().ToList();

            foreach (var category in Categories)
            {
                foreach (var product in category.Products)
                {
                    var price = inventory.FirstOrDefault(x => x.ProductId == product.Id).UnitPrice;
                    product.Price = price.ToMoney();

                    int discountRate = discounts?.FirstOrDefault(x => x.ProductId == product.Id)?.DiscountRate ?? 0;

                    product.DiscountRate = discountRate;

                    var discountAmount = Math.Round((price * discountRate) / 100);

                    product.PriceWhitDiscount = (price - discountAmount).ToMoney();

                    product.HasDiscount = discountRate > 0;
                }
            }

            return Categories;
        }

        private static List<ProductQueryModel> MapProduct(List<Product> products)
        {

            return products.Select(product => new ProductQueryModel
            {
                Id = product.Id,
                Name = product.Name,
                Category = product.Category.Name,
                CategorySlug = product.Category.Slug,
                Picture = product.Picture,
                PictureAlt = product.PictureAlt,
                PictureTitle = product.PictureTitle,
                Slug = product.Slug,
                IsNew = product.CreationDate.AddDays(3) > DateTime.Now ? true : false,

            }).ToList();

        }

        public ProductCategoryQueryModel GetProductCategoryWithProductsBy(string slug)
        {
            var inventory = _inventoryContext.inventory
                .Select(x => new { x.ProductId, x.UnitPrice })
                .AsNoTracking().ToList();

            var discount = _discountContext.customerDiscounts
                .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .Select(x => new { x.ProductId, x.DiscountRate ,x.EndDate })
                .AsNoTracking().ToList();

            var category = _shopContext.ProductCategories
                .Include(x => x.Products)
                .ThenInclude(x => x.Category)
                .Select(x => new ProductCategoryQueryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Keywords = x.Keywords,
                    MetaDescription = x.MetaDescription,
                    Slug = x.Slug,
                    Products = MapProduct(x.Products),
                }).AsNoTracking().FirstOrDefault(x => x.Slug == slug);

            foreach (var product in category.Products)
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
            return category;
        }
    }
}
