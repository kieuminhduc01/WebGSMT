using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebGSMT.Models;

namespace WebGSMT.Configurations
{
    public class Permission_RoleConfig: IEntityTypeConfiguration<Permission_Role>
    {
        public void Configure(EntityTypeBuilder<Permission_Role> builder)
        {
            builder.ToTable("Permission_Role");
            builder.HasKey(k =>new {k.PermissionID,k.RoleName});

            builder.Property(x => x.PermissionID).IsRequired();
            builder.Property(x => x.RoleName).IsRequired();
            builder.HasOne(acc => acc.Permission).WithMany(ars => ars.Permission_Roles).HasForeignKey(fk => fk.PermissionID);
            builder.HasOne(ro => ro.Role).WithMany(ars => ars.Permission_Roles).HasForeignKey(fk => fk.RoleName);
        }
    }
}
