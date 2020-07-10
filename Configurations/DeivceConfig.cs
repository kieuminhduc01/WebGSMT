using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebGSMT.Models;

namespace WebGSMT.Configurations
{
    public class DeivceConfig : IEntityTypeConfiguration<Device>
    {
        public void Configure(EntityTypeBuilder<Device> builder)
        {
            builder.ToTable("Device");
            builder.HasKey(k => k.Name);

            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.BranchOrProtocol).IsRequired();
            builder.HasMany(ro => ro.Datas).WithOne(de => de.Device).HasForeignKey(fk => fk.DeviceName);
        }
    }
}
