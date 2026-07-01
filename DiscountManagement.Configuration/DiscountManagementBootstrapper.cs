using DiscounrManagement.Domain.ColleagueDiscountAgg;
using DiscounrManagement.Domain.CustomerDiscountAgg;
using DiscountManagement.Application;
using DiscountManagement.Application.Contracts.ColleagueDiscountAgg;
using DiscountManagement.Application.Contracts.CustomerDiscountAgg;
using DiscountManagement.Infrastructure.EFCore;
using DiscountManagement.Infrastructure.EFCore.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DiscountManagement.Configuration
{
    public class DiscountManagementBootstrapper
    {
        public static void Configure(IServiceCollection services, string connectionString)
        {
            services.AddTransient<ICustomerDiscountApplication, CustomerDiscountApplication>();
            services.AddTransient<ICustomerDiscountRepository, CustomerDiscountRepository>();

            services.AddTransient<IColleagueDiscountApplication, ColleagueDiscountApplication>();
            services.AddTransient<IColleagueDiscountRepository, ColleagueDiscountRepository>();

            services.AddDbContext<DiscountContext>(options => options.UseSqlServer(connectionString));
        }
    }
}
