using _0_Framework.Application;
using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations;

namespace ShopManagement.Application.Contracts.ProductPictureAgg
{
    public class CreateProductPicture
    {
        [Range(1,double.MaxValue,ErrorMessage =ValidationMessages.IsRequired)]
        public long ProductId { get; set; }

        [Required(ErrorMessage =ValidationMessages.IsRequired)]
        public IFormFile Picture { get; set; }
   
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string PictureAlt { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string PictureTitle { get; set; }
    }
}
