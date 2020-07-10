using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebGSMT.Models;

namespace WebGSMT.Configurations
{
    public class Catalog_DataConfig : IEntityTypeConfiguration<Catalog_Data>
    {
        public void Configure(EntityTypeBuilder<Catalog_Data> builder)
        {
            builder.ToTable("Catalog_Data");
            builder.HasKey(k => k.TagName);

            builder.Property(x => x.TagName).IsRequired();
            builder.Property(x => x.DeviceName).IsRequired();
            builder.Property(x => x.Unit).IsRequired();

            builder.HasMany(ro => ro.Datas).WithOne(de => de.Catalog_Data).HasForeignKey(fk => fk.TagName);
        }
    }
}
