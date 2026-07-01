using _0_Framework.Infrastucture;
using AccountManagement.Domain.AccountAgg;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Accountmanagement.Infrasturecture.EFCore.Repository
{
    public class AccountRepository : RepositoryBase<long, Account>, IAccountRepository
    {
        private readonly AccountContext _context;
        public AccountRepository(AccountContext context) : base(context)
        {
            _context = context;
        }

        public Account GetBy(string userName)
        {
            return _context.Accounts.FirstOrDefault(x => x.UserName == userName);
        }

        public List<Account> Search(string fullName, string userName, string mobile,long roleId)
        {
            var query = _context.Accounts.AsQueryable();

            if (!string.IsNullOrWhiteSpace(fullName))
                query = query.Where(x => x.FullName == fullName);
            
            if (!string.IsNullOrWhiteSpace(userName))
                query = query.Where(x => x.UserName == userName);
            
            if (!string.IsNullOrWhiteSpace(mobile))
                query = query.Where(x => x.Mobile == mobile);
            if(roleId > 0)
                query = query.Where(x => x.RoleId == roleId);


            return query.Include(x => x.Role).OrderByDescending(x => x.Id).ToList();
        }
    }
}
