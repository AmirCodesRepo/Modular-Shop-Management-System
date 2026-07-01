using _0_Framework.Domain;
using System.Linq;

namespace ShopManagement.Domain.SlideAgg
{
    public interface ISlideRepository : IRepository<long,Slide>
    {
        IQueryable<Slide> Search(string heading,int IsRemoved);
    }
}
