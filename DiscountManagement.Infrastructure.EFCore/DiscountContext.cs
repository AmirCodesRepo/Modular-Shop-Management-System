using DiscounrManagement.Domain.ColleagueDiscountAgg;
using DiscounrManagement.Domain.CustomerDiscountAgg;
using DiscountManagement.Infrastructure.EFCore.Mapping;
using Microsoft.EntityFrameworkCore;

namespace DiscountManagement.Infrastructure.EFCore
{
    public class DiscountContext : DbContext
    {
        public DbSet<CustomerDiscount> customerDiscounts { get; set; }
        public DbSet<ColleagueDiscount> colleagueDiscounts { get; set; }
        public DiscountContext(DbContextOptions<DiscountContext> options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var assembly = typeof(CustomerDiscountMapping).Assembly;
            
            modelBuilder.ApplyConfigurationsFromAssembly(assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
