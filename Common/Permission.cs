using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebGSMT.Common
{
    public static class Permission
    {
        public static int ConvertPermissionStringToID(string PermissionStr)
        {
            WebGSMT.Models.GiamSatMoiTruongDbContext _context = new Models.GiamSatMoiTruongDbContext();
            int ID=0;
            var permission = _context.Permissions.FirstOrDefault(x=>x.Text==PermissionStr);
            if (permission != null)
            {
                ID = permission.ID;
            }
            return ID;
        }
    }
}
