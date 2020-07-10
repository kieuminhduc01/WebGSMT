using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebGSMT.Models;

namespace WebGSMT.Configurations
{
    public class Account_RoleConfig : IEntityTypeConfiguration<Account_Role>
    {

        public void Configure(EntityTypeBuilder<Account_Role> builder)
        {
            builder.ToTable("Account_Role");
            builder.HasKey(x => new { x.UserName ,x.RoleName});

            builder.Property(x => x.UserName).IsRequired();
            builder.Property(x => x.RoleName).IsRequired();

            builder.HasOne(acc => acc.Account).WithMany(ars => ars.Account_Roles).HasForeignKey(fk => fk.UserName);
            builder.HasOne(ro => ro.Role).WithMany(ars => ars.Account_Roles).HasForeignKey(fk => fk.RoleName);
        }
    }
}
