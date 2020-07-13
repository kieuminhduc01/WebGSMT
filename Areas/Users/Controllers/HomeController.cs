using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebGSMT.Models;

namespace WebGSMT.Areas.Users.Controllers
{
    public class HomeController : Controller
    {
        GiamSatMoiTruongDbContext _db;
        public HomeController(GiamSatMoiTruongDbContext db)
        {
            _db = db;
        }

        [Area("Users")]
        public async Task<IActionResult> Index()
        {
            var data = await _db.Datas.ToListAsync();
            ViewBag.TagName = await _db.Catalog_Datas.ToListAsync();
            return View(data);
        }
    }
}