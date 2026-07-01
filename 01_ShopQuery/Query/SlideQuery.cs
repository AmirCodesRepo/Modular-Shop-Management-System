using _01_ShopQuery.Contracts.Slide;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Infrastructure.EFCore;
using System.Collections.Generic;
using System.Linq;

namespace _01_ShopQuery.Query
{
    public class SlideQuery : ISlideQuery
    {
        private readonly ShopContext _shopContext;
        public SlideQuery(ShopContext shopContext)
        {
            _shopContext = shopContext;
        }
        public List<SlideQueryModel> GetSlides()
        {
            return _shopContext.Slides.Where(x => x.IsRemoved == false)
                .Select(x => new SlideQueryModel
                {
                    Heading = x.Heading,
                    Title = x.Title,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Text = x.Text,
                    Link = x.Link,
                    BtnText = x.BtnText
                }).AsNoTracking().ToList();
        }
    }
}
