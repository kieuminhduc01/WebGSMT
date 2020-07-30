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
            if (PermissionStr == "Admin")
            {
                return 1;
            }

            return 0;//khi không có quyền nào trùng với quy định
        }
    }
}
