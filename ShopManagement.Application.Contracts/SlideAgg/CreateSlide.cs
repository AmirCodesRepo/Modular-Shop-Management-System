using _0_Framework.Application;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ShopManagement.Application.Contracts.SlideAgg
{
    public class CreateSlide : SlideBase
    {
        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public IFormFile Picture { get; set; }

    }
}
