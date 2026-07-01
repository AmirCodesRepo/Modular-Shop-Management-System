using _0_Framework.Application;
using System.Collections.Generic;

namespace AccountManagement.Application.Contracts.AccountAgg
{
    public interface IAccountApplication
    {
        OperationResult Register(RegisterAccount command);
        OperationResult Edit(EditAccount command);
        OperationResult ChangePassword(ChangePassword command);
        OperationResult LoginUser(Login command);
        EditAccount GetDetailes(long id);
        void Signout();
        List<AccountViewModel> Search(AccountSearchModel searchModel);
    }
}
