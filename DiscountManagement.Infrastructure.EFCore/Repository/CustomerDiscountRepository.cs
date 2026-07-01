using _0_Framework.Application;
using _0_Framework.Infrastucture;
using DiscounrManagement.Domain.CustomerDiscountAgg;
using System.Collections.Generic;
using System.Linq;

namespace DiscountManagement.Infrastructure.EFCore.Repository
{
    public class CustomerDiscountRepository : RepositoryBase<long, CustomerDiscount>, ICustomerDiscountRepository
    {
        private readonly DiscountContext _context;
        public CustomerDiscountRepository(DiscountContext context) : base(context)
        {
            _context = context;
        }
        public List<CustomerDiscount> Search(long productId, string startDate, string endDate)
        {
            var query = _context.customerDiscounts.AsQueryable();

            if (productId > 0)
            {
                query = query.Where(x => x.ProductId == productId);
            }
            if (!string.IsNullOrWhiteSpace(startDate))
            {
                var start = startDate.ToGeorgianDateTime();
                query = query.Where(x => x.StartDate >= start);

            }
            if (!string.IsNullOrWhiteSpace(endDate))
            {
                var end = endDate.ToGeorgianDateTime();
                
                query = query.Where(x => x.EndDate <= end);
            }

            return query.OrderByDescending(x => x.Id).ToList();
        }
    }
}
