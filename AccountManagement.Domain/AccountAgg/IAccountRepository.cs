using _0_Framework.Domain;
using System.Collections.Generic;

namespace AccountManagement.Domain.AccountAgg
{
    public interface IAccountRepository : IRepository<long, Account>
    {
        Account GetBy(string userName);
        List<Account> Search(string fullName, string userName, string mobile,long roleId);
    }
}
