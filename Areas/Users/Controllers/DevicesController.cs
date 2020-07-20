using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebGSMT.Models;

namespace WebGSMT.Areas.Users
{
    [Area("Users")]
    [Route("{area}/Devices")]
    public class DevicesController : Controller
    {
        GiamSatMoiTruongDbContext _db;
        public DevicesController(GiamSatMoiTruongDbContext db)
        {
            _db = db;
        }

        [Route("ListDevice")]
        public async Task<IActionResult> Index()
        {
            return  View(await _db.Devices.ToListAsync());
        }
    }
}