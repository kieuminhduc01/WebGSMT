using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebGSMT.Models;

namespace WebGSMT.Configurations
{
    public class DataConfig : IEntityTypeConfiguration<Data>
    {
        public void Configure(EntityTypeBuilder<Data> builder)
        {
            builder.ToTable("Data");
            builder.HasKey(k => new { k.TagName,k.DeviceName,k.Time} );

            builder.Property(x => x.TagName).IsRequired();
            builder.Property(x => x.DeviceName).IsRequired();
            builder.Property(x => x.Time).IsRequired();
          
        }
    }
}
