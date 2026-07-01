using _0_Framework.Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscountManagement.Application.Contracts.CustomerDiscountAgg
{
    public interface ICustomerDiscountApplication
    {
        OperationResult Define(DefineCustomerDiscount command);
        OperationResult Eidt(EditCustomerDiscount command);
        EditCustomerDiscount GetDetailes(long id);
        List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel searchModel);

    }
}
