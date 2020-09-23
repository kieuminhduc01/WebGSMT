using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Threading.Tasks;
using WebGSMT.Common;
using WebGSMT.Models;

namespace WebGSMT.Extensions
{
    public static class ModelBuilderExtentions
    {

        public static void Permission_Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Models.Permission>().HasData(
                new Models.Permission
                {
                    ID = 1,
                    Parent = "#",
                    Text = "Giam Sat",
                },
                new Models.Permission
                {
                    ID = 2,
                    Parent = "1",
                    Text = "Thiet bi",
                },

                new Models.Permission
                {
                    ID = 3,
                    Parent = "2",
                    Text = "Thiet bi-Them moi",
                }, new Models.Permission
                {
                    ID = 4,
                    Parent = "2",
                    Text = "Thiet bi-Xem",
                }, new Models.Permission
                {
                    ID = 5,
                    Parent = "2",
                    Text = "Thiet bi-Sua",
                }, new Models.Permission
                {
                    ID = 6,
                    Parent = "2",
                    Text = "Thiet bi-Xoa",
                }, new Models.Permission
                {
                    ID = 7,
                    Parent = "1",
                    Text = "Danh muc du lieu",
                }, new Models.Permission
                {
                    ID = 8,
                    Parent = "7",
                    Text = "Danh muc du lieu-Them moi",
                }, new Models.Permission
                {
                    ID = 9,
                    Parent = "7",
                    Text = "Danh muc du lieu-Sua",
                }, new Models.Permission
                {
                    ID = 10,
                    Parent = "7",
                    Text = "Danh muc du lieu-Xoa",
                }, new Models.Permission
                {
                    ID = 11,
                    Parent = "1",
                    Text = "Quan Tri Vien",
                }, new Models.Permission
                {
                    ID = 12,
                    Parent = "11",
                    Text = "Quan Tri Vien-Vai Tro",
                }, new Models.Permission
                {
                    ID = 13,
                    Parent = "12",
                    Text = "Quan Tri Vien-Vai Tro-Them moi",
                }, new Models.Permission
                {
                    ID = 14,
                    Parent = "12",
                    Text = "Quan Tri Vien-Vai Tro-Xem",
                }, new Models.Permission
                {
                    ID = 15,
                    Parent = "12",
                    Text = "Quan Tri Vien-Vai Tro-Sua",
                }, new Models.Permission
                {
                    ID = 16,
                    Parent = "12",
                    Text = "Quan Tri Vien-Vai Tro-Xoa",
                },
                new Models.Permission
                {
                    ID = 17,
                    Parent = "11",
                    Text = "Quan Tri Vien-Nguoi Dung",
                },
                new Models.Permission
                {
                    ID = 18,
                    Parent = "17",
                    Text = "Quan Tri Vien-Nguoi Dung-Them moi",
                }, new Models.Permission
                {
                    ID = 19,
                    Parent = "17",
                    Text = "Quan Tri Vien-Nguoi Dung-Xem",
                }, new Models.Permission
                {
                    ID = 20,
                    Parent = "17",
                    Text = "Quan Tri Vien-Nguoi Dung-Sua",
                }, new Models.Permission
                {
                    ID = 21,
                    Parent = "17",
                    Text = "Quan Tri Vien-Nguoi Dung-Xoa",
                }
            );

        }
        public static void Role_Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(
                new Role
                {
                    RoleName = "Admin",
                    Description = "lam duoc moi thu"
                }
            );
        }
        public static void Permission_Role_Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Permission_Role>().HasData(
                new Permission_Role
                {
                    RoleName = "Admin",
                    PermissionID = 1
                }, new Permission_Role
                {
                    RoleName = "Admin",
                    PermissionID = 2
                }, new Permission_Role
                {
                    RoleName = "Admin",
                    PermissionID = 3
                }, new Permission_Role
                {
                    RoleName = "Admin",
                    PermissionID = 4
                }, new Permission_Role
                {
                    RoleName = "Admin",
                    PermissionID = 5
                }, new Permission_Role
                {
                    RoleName = "Admin",
                    PermissionID = 6
                }, new Permission_Role
                {
                    RoleName = "Admin",
                    PermissionID = 7
                }, new Permission_Role
                {
                    RoleName = "Admin",
                    PermissionID = 8
                }, new Permission_Role
                {
                    RoleName = "Admin",
                    PermissionID = 9
                }, new Permission_Role
                {
                    RoleName = "Admin",
                    PermissionID = 10
                }, new Permission_Role
                {
                    RoleName = "Admin",
                    PermissionID = 11
                }, new Permission_Role
                {
                    RoleName = "Admin",
                    PermissionID = 12
                }, new Permission_Role
                {
                    RoleName = "Admin",
                    PermissionID = 13
                }, new Permission_Role
                {
                    RoleName = "Admin",
                    PermissionID = 14
                }, new Permission_Role
                {
                    RoleName = "Admin",
                    PermissionID = 15
                }, new Permission_Role
                {
                    RoleName = "Admin",
                    PermissionID = 16
                }, new Permission_Role
                {
                    RoleName = "Admin",
                    PermissionID = 17
                }, new Permission_Role
                {
                    RoleName = "Admin",
                    PermissionID = 18
                }, new Permission_Role
                {
                    RoleName = "Admin",
                    PermissionID = 19
                }, new Permission_Role
                {
                    RoleName = "Admin",
                    PermissionID = 20
                }, new Permission_Role
                {
                    RoleName = "Admin",
                    PermissionID = 21
                }
            );
        }
        public static void Account_Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account>().HasData(
                 new Account()
                 {
                     UserName = "duckieuola",
                     Password = "123456789",
                     FullName = "Kieu Minh Duc",
                     DOB = DateTime.Parse("1999/05/01"),
                     Email = "duckmhe130998@fpt.edu.vn",
                     PhoneNumber = "0377398442",
                     Active = true
                 },
                 new Account()
                 {
                     UserName = "duc_ta_vl",
                     Password = "123456789",
                     FullName = "Ta Vu Anh Duc",
                     DOB = DateTime.Parse("1999/05/01"),
                     Email = "duckmhe130998@fpt.edu.vn",
                     PhoneNumber = "0377398442",
                     Active = true
                 }

            );
            
        }
        public static void Account_Role_Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Account_Role>().HasData(
                 new Account_Role()
                 {
                     UserName = "duckieuola",
                     RoleName = "Admin"
                 }
            );
        }
    }
}
