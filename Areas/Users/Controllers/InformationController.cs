using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebGSMT.ActionFilter;
using WebGSMT.Common;
using WebGSMT.Models;

namespace WebGSMT.Areas.Users.Controllers
{
    [Area("Users")]
    [Route("{area}/Information")]
    [ServiceFilter(typeof(AuthorizeActionFilter))]
    public class InformationController : Controller
    {
        private GiamSatMoiTruongDbContext _db = new GiamSatMoiTruongDbContext();

        public InformationController(GiamSatMoiTruongDbContext db)
        {
            _db = db;
        }
        public  IActionResult Index()
        {
            var infor =  _db.Accounts.FirstOrDefault(x=>x.UserName == HttpContext.Session.GetString("UserName").ToString());
            return View(infor);
        }
        
        [HttpPost]
        [Route("edit")]
        public string Edit(string FullName, string Email, string Dob, string phoneNumber)
        {
            if (ModelState.IsValid)
            {
                  var account = _db.Accounts.FirstOrDefault(x => x.UserName == HttpContext.Session.GetString("UserName").ToString());
                    if (account != null)
                    {
                        account.FullName = FullName;
                        account.Email = Email;
                        account.DOB = DateTime.ParseExact(Dob, "MM/dd/yyyy", null); ;
                        account.PhoneNumber = phoneNumber;
                    }
                    _db.SaveChanges();
                    HttpContext.Session.SetObjectAsJson("Account", account);
                return "success";
            }
            return "fali";
        }
    }
}