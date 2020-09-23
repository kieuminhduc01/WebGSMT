using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebGSMT.Configurations;
using WebGSMT.Extensions;

namespace WebGSMT.Models
{
    public class GiamSatMoiTruongDbContext : DbContext
    {
        public GiamSatMoiTruongDbContext(DbContextOptions<GiamSatMoiTruongDbContext> options) : base(options)
        {

        }

        public GiamSatMoiTruongDbContext()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
    => options.UseSqlite("DataSource=GS.db");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new Account_RoleConfig());
            modelBuilder.ApplyConfiguration(new AccountConfig());
            modelBuilder.ApplyConfiguration(new Catalog_DataConfig());
            modelBuilder.ApplyConfiguration(new DataConfig());
            modelBuilder.ApplyConfiguration(new DeivceConfig());
            modelBuilder.ApplyConfiguration(new Permission_RoleConfig());
            modelBuilder.ApplyConfiguration(new PermissonConfig());
            modelBuilder.ApplyConfiguration(new RoleConfig());

            modelBuilder.Permission_Seed();// tạo dữ liệu ban đầu
            modelBuilder.Role_Seed();
            modelBuilder.Permission_Role_Seed();
            modelBuilder.Account_Seed();
            modelBuilder.Account_Role_Seed();
        }
       
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Account_Role> Account_Roles { get; set; }
        public DbSet<Catalog_Data> Catalog_Datas { get; set; }
        public DbSet<Data> Datas { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Permission_Role> AccoPermission_Roles { get; set; }
        public DbSet<Role> Roles { get; set; }
       
    }
}
