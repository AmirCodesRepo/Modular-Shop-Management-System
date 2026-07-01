using Accountmanagement.Infrasturecture.EFCore;
using Accountmanagement.Infrasturecture.EFCore.Repository;
using AccountManagement.Application;
using AccountManagement.Application.Contracts.AccountAgg;
using AccountManagement.Application.Contracts.RoleAgg;
using AccountManagement.Domain.AccountAgg;
using AccountManagement.Domain.RoleAgg;
using AccountManagement.Infrasturecture.EFCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AccountManagement.Configuration
{
    public class AccountManagementBootstrapper
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            services.AddTransient<IAccountApplication, AccountApplication>();
            services.AddTransient<IAccountRepository, AccountRepository>();

            services.AddTransient<IRoleApplication, RoleApplication>();
            services.AddTransient<IRoleRepository, RoleRepository>();

            services.AddDbContext<AccountContext>(options => options.UseSqlServer(connectionString));
        }

    }
}
