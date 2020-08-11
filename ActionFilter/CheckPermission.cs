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
            bool Access = false;
            string username = context.HttpContext.Session.GetString("UserName");
            List<string> permissions = new List<string>();
            List<Account_Role> accRoleTemp = _context.Account_Roles.Where(x => x.UserName == username).ToList<Account_Role>();
            if (accRoleTemp != null)
            {
                List<Permission_Role> lstPerRo;
                foreach (Account_Role acc in accRoleTemp)
                {
                    lstPerRo = _context.AccoPermission_Roles.Where(x => x.RoleName == acc.RoleName).ToList();
                    foreach (Permission_Role perRoleUnit in lstPerRo)
                    {
                        if (perRoleUnit.PermissionID == PermissionID)
                        {
                            Access = true;
                        }
                    }
                }

            }

            if (!Access)
            {
                context.Result = new RedirectResult("/Authenticate/Login");
            }

        }

    }
    public class AuthorizePermission : TypeFilterAttribute
    {
        public AuthorizePermission(string permission)
            : base(typeof(AuthorizeActionPemissionFilter))
        {
            Arguments = new object[] { permission };
        }
    }
}
