using Microsoft.AspNetCore.Http;

namespace ShopManagement.Application.Contracts.SlideAgg
{
    public class EditSlide : SlideBase
    {
        public long Id { get; set; }
        public IFormFile? Picture { get; set; }
    }
}
