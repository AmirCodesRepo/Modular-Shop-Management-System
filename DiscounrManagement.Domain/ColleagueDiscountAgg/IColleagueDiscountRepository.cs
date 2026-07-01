using _0_Framework.Domain;
using System.Collections.Generic;

namespace DiscounrManagement.Domain.ColleagueDiscountAgg
{
    public interface IColleagueDiscountRepository :IRepository<long ,ColleagueDiscount>
    {
        List<ColleagueDiscount> Search(long productId);
    }
}
