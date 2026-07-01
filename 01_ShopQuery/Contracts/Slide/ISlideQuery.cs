using System.Collections.Generic;

namespace _01_ShopQuery.Contracts.Slide
{
    public interface ISlideQuery
    {
        List<SlideQueryModel> GetSlides();
    }
}
