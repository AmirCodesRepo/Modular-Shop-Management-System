using _0_Framework.Domain;
using System.Collections.Generic;

namespace DiscounrManagement.Domain.CustomerDiscountAgg
{
    public interface ICustomerDiscountRepository : IRepository<long, CustomerDiscount>
    {
        List<CustomerDiscount> Search(long productId,string startDate,string endDate);
    }
}
