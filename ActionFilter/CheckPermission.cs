using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebGSMT.Common;
using WebGSMT.Models;

namespace WebGSMT.ActionFilter
{
    public class AuthorizeActionPemissionFilter : IAuthorizationFilter
    {
        public int PermissionID { get; set; }
        GiamSatMoiTruongDbContext _context = new GiamSatMoiTruongDbContext();

        public AuthorizeActionPemissionFilter(string PermissionName)
        {
            this.PermissionID = Common.Permission.ConvertPermissionStringToID(PermissionName);

        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string userName = context.HttpContext.Session.GetString("UserName");
            Account_Role accRoleTemp = _context.Account_Roles.FirstOrDefault(x => x.UserName == userName);
            List<Permission_Role> lstPerRo = _context.AccoPermission_Roles.Where(x => x.RoleName == accRoleTemp.RoleName).ToList();

            bool Access = false;
            foreach (Permission_Role perRoleUnit in lstPerRo)
            {
                if (perRoleUnit.PermissionID == PermissionID)
                {
                    Access = true;
                }
            }
            if (!Access)
            {
                context.Result = new UnauthorizedResult();
            }

        }

    }
    public class AuthorizeAttribute : TypeFilterAttribute
    {
        public AuthorizeAttribute(string permission)
            : base(typeof(AuthorizeActionPemissionFilter))
        {
            Arguments = new object[] { permission };
        }
    }
}
