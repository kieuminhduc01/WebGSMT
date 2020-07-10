using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebGSMT.Models
{
    public class GiamSatMoiTruongDbContextFactory : IDesignTimeDbContextFactory<GiamSatMoiTruongDbContext>
    {
        public GiamSatMoiTruongDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<GiamSatMoiTruongDbContext>();
            optionsBuilder.UseSqlite(connectionString);

            return new GiamSatMoiTruongDbContext(optionsBuilder.Options);
        }
    }
}
