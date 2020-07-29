using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
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
            var data = await (from d in _db.Datas
                              join c in _db.Catalog_Datas on d.TagName equals c.TagName
                              select new { TagName = d.TagName, Time =  d.Time, Value = d.Value, Unit = c.Unit, Connected = d.Connected }).ToListAsync();
            ViewBag.TagName = await _db.Catalog_Datas.ToListAsync();

            List<DataDevice> dataDevices = new List<DataDevice>();
            foreach (var item in data)
            {
                DataDevice dataDevice = new DataDevice();
                dataDevice.TagName = item.TagName;
                dataDevice.Time = item.Time;
                dataDevice.Value = item.Value;
                dataDevice.Unit = item.Unit;
                dataDevice.Connected = item.Connected;
                dataDevices.Add(dataDevice);
            }
            //ViewBag.DataDeivce = data;
             
            var listUnit = _db.Catalog_Datas.Select(x => x.Unit).Distinct();
            ViewBag.ListUnit = new SelectList(listUnit);
           ViewBag.TagName = await _db.Catalog_Datas.ToListAsync();
            
            return View(dataDevices);
        }

        public class DataDevice
        {
            public string TagName { get; set; }
            public DateTime Time { get; set; }
            public double Value { get; set; }
            public string Unit { get; set; }
            public bool Connected { get; set; }
        } 
    }
}