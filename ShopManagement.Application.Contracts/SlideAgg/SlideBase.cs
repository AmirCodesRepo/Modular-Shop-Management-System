using _0_Framework.Application;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace ShopManagement.Application.Contracts.SlideAgg
{
    public class SlideBase
    {

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string PictureAlt { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string PictureTitle { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Heading { get; set; }

        public string Title { get; set; }
        public string Text { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string BtnText { get; set; }

        [Required(ErrorMessage = ValidationMessages.IsRequired)]
        public string Link { get; set; }
    }
}
