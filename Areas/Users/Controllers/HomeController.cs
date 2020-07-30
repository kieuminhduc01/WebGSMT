using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using WebGSMT.ActionFilter;
using WebGSMT.Models;

namespace WebGSMT.Areas.Users.Controllers
{
    [Area("Users")]
    [Route("{area}/Home")]
    [ServiceFilter(typeof(AuthorizeActionFilter))]
    public class HomeController : Controller
    {
        GiamSatMoiTruongDbContext _db;
        public HomeController(GiamSatMoiTruongDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        [Route("Index")]
        public async Task<IActionResult> Index(string PLC)
        {
            var data = await (from d in _db.Datas
                              join c in _db.Catalog_Datas on d.TagName equals c.TagName
                              select new { TagName = d.TagName, DeviceName = d.DeviceName, Time = d.Time, Value = d.Value, Unit = c.Unit, Connected = d.Connected })
                              .Where(s => s.DeviceName.Contains(PLC)).ToListAsync();



            List<DataDevice> dataDevices = new List<DataDevice>();
            foreach (var item in data)
            {
                DataDevice dataDevice = new DataDevice();
                dataDevice.TagName = item.TagName;
                dataDevice.DeviceName = item.DeviceName;
                dataDevice.Time = item.Time;
                dataDevice.Value = item.Value;
                dataDevice.Unit = item.Unit;
                dataDevice.Connected = item.Connected;
                dataDevices.Add(dataDevice);
            }
            //ViewBag.DataDeivce = data;
            //List Tag Name
            var list = _db.Datas.Join(_db.Catalog_Datas,
                d => d.TagName,
                c => c.TagName,
                (datas, catalogs) => new { datas, catalogs })
                .Where(m => m.datas.TagName == m.catalogs.TagName && m.datas.Connected == true && m.datas.DeviceName == PLC)
                .Select(m => new TagNameAndUnit { TagName = m.datas.Catalog_Data.TagName, Unit = m.datas.Catalog_Data.Unit }).Distinct().ToList();
            ViewBag.TagName = list;

            var listUnit = _db.Catalog_Datas.Select(x => x.Unit).Distinct();
            ViewBag.ListUnit = new SelectList(listUnit);


            return View(dataDevices);
        }
        public class TagNameAndUnit
        {
            public string TagName { get; set; }
            public string Unit { get; set; }
        }
        public class DataDevice
        {
            public string TagName { get; set; }
            public string DeviceName { get; set; }
            public DateTime Time { get; set; }
            public double Value { get; set; }
            public string Unit { get; set; }
            public bool Connected { get; set; }

        }
    }
}