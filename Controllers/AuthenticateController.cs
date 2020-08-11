using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebGSMT.Common;
using WebGSMT.Models;
using WebGSMT.ModelView.Account;

namespace WebGSMT.Controllers
{
    [Route("Authenticate")]
    [AllowAnonymous]
    public class AuthenticateController : Controller
    {
        private GiamSatMoiTruongDbContext _db = new GiamSatMoiTruongDbContext();


        public AuthenticateController(GiamSatMoiTruongDbContext db)
        {
            _db = db;
        }
        [Route("Login")]
        [HttpGet]
        public IActionResult Login()
        {

            return View();
        }

        [Route("Login")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(AuthenticateModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            // Calll tới service authenticate để check login có tành công không?
            else
            {
                var authenticatedUser = await _db.Accounts.SingleOrDefaultAsync(m => m.UserName == model.Username && m.Password == model.Password);
                if (authenticatedUser != null)
                {
                    if (authenticatedUser.Active == false)
                    {
                        TempData["Message"] = "Tài khoản đã này đã bị khóa! ";
                        return Redirect("Login");
                    }
                    HttpContext.Session.SetString("UserName", authenticatedUser.UserName);
                    HttpContext.Session.SetObjectAsJson("Account", authenticatedUser);
                    return RedirectToAction("Index", "Home", new { area = "Users" });
                }
                else
                {
                    TempData["Message"] = "Mật Khẩu hoặc tài khoản sai ";
                    return Redirect("Login");
                }
                // Nếu không thì lại direct về trang login
            }
        }

        // logout
        [Route("Logout")]
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("UserName");
            return RedirectToAction("Login", "Authenticate");
        }
        [Route("permissions")]
        [HttpGet]
        public List<string> Permissions()
        {
            string username = HttpContext.Session.GetString("UserName");
            List<string> permissions = new List<string>();
            List<Account_Role> accRoleTemp = _db.Account_Roles.Where(x => x.UserName == username).ToList<Account_Role>();
            if (accRoleTemp != null)
            {
                List<Permission_Role> lstPerRo;
                foreach (Account_Role acc in accRoleTemp)
                {
                    lstPerRo = _db.AccoPermission_Roles.Where(x => x.RoleName == acc.RoleName).ToList();
                    foreach (Permission_Role perRoleUnit in lstPerRo)
                    {
                        string perText = _db.Permissions.Where(x => x.ID == perRoleUnit.PermissionID).Select(x => x.Text).FirstOrDefault();
                        permissions.Add(perText);
                    }
                }

            }
            return permissions.Distinct().ToList();
        }
    }
}