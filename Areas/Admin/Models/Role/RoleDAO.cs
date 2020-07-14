using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebGSMT.Models;

namespace WebGSMT.Areas.Admin.Models.Role
{
    public class RoleDAO
    {
        static GiamSatMoiTruongDbContext db = new GiamSatMoiTruongDbContext();
        public static bool checkRoleName(string role)
        {
            foreach (var i in db.Roles)
            {
                if (i.RoleName.ToLower().Equals(role.ToLower()))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
