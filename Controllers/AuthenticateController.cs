using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
               if(authenticatedUser != null)
                {
                    HttpContext.Session.SetString("UserName", authenticatedUser.UserName);
                    return RedirectToAction("Index", "Home");
                }
                else
                {                                                            
                    TempData["Message"] = "Incorect username or password";
                    return Redirect("Login");
                }
                // Nếu không thì lại direct về trang login
            }
        }

        // logout
        [Route("logout")]
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("UserName");
            return RedirectToAction("Login","User");
        }

    }


}