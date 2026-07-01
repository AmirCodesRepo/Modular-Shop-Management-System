using _0_Framework.Application;
using Microsoft.AspNetCore.Http;
using ShopManagement.Application.Contracts.ProductCategoryAgg;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ShopManagement.Application.Contracts.ProductAgg
{
    public class CreateProduct
    {
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Name { get; set; }
        
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Code { get; set; }


        public bool IsInStock { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string ShortDescription { get; set; }

        public string Description { get; set; }

        [FileExtensionLimitation(new[] { ".jpeg", ".png",".jpg"}, ErrorMessage = ValidationMessages.InvalidFileType)]
        [MaxFileSize(2 * 1024 * 1024, ErrorMessage = ValidationMessages.InvalidFileSize)]
        public IFormFile Picture { get; set; }

        public string PictureAlt { get; set; }
        public string PictureTitle { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Keywords { get; set; }
        
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string MetaDescription { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Slug { get; set; }

        [Range(1,10000,ErrorMessage = ValidationMessages.IsRequired)]
        public long CategoryId { get; set; }
        
        public List<ProductCategoryViewModel> ProductCategories { get; set; }
    }
}
