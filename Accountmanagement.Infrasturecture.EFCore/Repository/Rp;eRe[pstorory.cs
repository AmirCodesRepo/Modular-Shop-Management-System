using _0_Framework.Infrastucture;
using Accountmanagement.Infrasturecture.EFCore;
using AccountManagement.Domain.RoleAgg;

namespace AccountManagement.Infrasturecture.EFCore.Repository
{
    public class RoleRepository : RepositoryBase<long, Role>, IRoleRepository
    {
        private readonly AccountContext _context;
        public RoleRepository(AccountContext context) : base(context)
        {
            _context = context;
        }
    }
}
