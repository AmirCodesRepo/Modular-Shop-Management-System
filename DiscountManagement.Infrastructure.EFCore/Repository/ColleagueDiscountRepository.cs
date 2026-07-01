using _0_Framework.Infrastucture;
using DiscounrManagement.Domain.ColleagueDiscountAgg;
using System.Collections.Generic;
using System.Linq;

namespace DiscountManagement.Infrastructure.EFCore.Repository
{
    public class ColleagueDiscountRepository : RepositoryBase<long, ColleagueDiscount>, IColleagueDiscountRepository
    {
        private readonly DiscountContext _context;
        public ColleagueDiscountRepository(DiscountContext context) : base(context)
        {
            _context = context;
        }
        public List<ColleagueDiscount> Search(long productId)
        {
            var query = _context.colleagueDiscounts.AsQueryable();
            if(productId > 0)
            {
                query = query.Where(x => x.ProductId == productId);
            }

            return query.ToList();
        }
    }
}
