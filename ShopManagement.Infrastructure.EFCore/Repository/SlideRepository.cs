using _0_Framework.Infrastucture;
using ShopManagement.Domain.SlideAgg;
using System.Linq;

namespace ShopManagement.Infrastructure.EFCore.Repository
{
    public class SlideRepository : RepositoryBase<long, Slide>, ISlideRepository
    {
        private readonly ShopContext _context;

        public SlideRepository(ShopContext context) : base(context)
        {
            _context = context;
        }
        public IQueryable<Slide> Search(string heading, int IsRemoved)
        {
            var query = _context.Slides.AsQueryable();

            if (!string.IsNullOrEmpty(heading))
                query = query.Where(x => x.Heading.Contains(heading));

            if (IsRemoved == 1 || IsRemoved == 2)
            {
                var commmand = IsRemoved == 1 ? true : false;

                query = query.Where(x => x.IsRemoved == commmand);
            }

            return query.OrderByDescending(x => x.Id);
        }
    }
}
