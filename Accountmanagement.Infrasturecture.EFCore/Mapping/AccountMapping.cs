using AccountManagement.Domain.AccountAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Accountmanagement.Infrasturecture.EFCore.Mapping
{
    public class AccountMapping : IEntityTypeConfiguration<Account>
    {
        public void Configure(EntityTypeBuilder<Account> builder)
        {
            builder.HasKey(x => x.Id);
            builder.ToTable("Accounts");

            builder.Property(x => x.FullName).HasMaxLength(100).IsRequired();
            builder.Property(x => x.UserName).HasMaxLength(100).IsRequired();
            builder.Property(x => x.Password).HasMaxLength(1000).IsRequired();
            builder.Property(x => x.ProfilePhoto).HasMaxLength(500);
            builder.Property(x => x.Mobile).HasMaxLength(20).IsRequired();

            builder.HasOne(x => x.Role).WithMany(x => x.Accounts).HasForeignKey(x => x.RoleId);
        }
    }
}
