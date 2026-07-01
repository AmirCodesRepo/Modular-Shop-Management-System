using _0_Framework.Application;
using System.Collections.Generic;

namespace AccountManagement.Application.Contracts.RoleAgg
{
    public interface IRoleApplication
    {
        OperationResult Create(CreateRole command);
        OperationResult Edit(EditRole command);
        List<RoleViewModel> List();
        EditRole GetDetailes(long id);

    }
}
