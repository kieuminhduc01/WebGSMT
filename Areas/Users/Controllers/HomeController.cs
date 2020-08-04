using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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

            var listUnitDevice = _db.Catalog_Datas.Select(x => new { x.Unit, x.DeviceName }).Distinct();
            ViewBag.ListUnitDevice = new SelectList(listUnitDevice);

            var listDevice = _db.Catalog_Datas.Select(x => x.DeviceName).Distinct();
            ViewBag.ListDevice = new SelectList(listDevice);
            return View();
        }
        [HttpGet]
        [Route("units")]
        public List<string> GetUnitByDevice(string PLC)
        {
            var listUnitDevice = _db.Catalog_Datas.Where(x=>x.DeviceName==PLC).Select(x => x.Unit).Distinct().ToList();
            return listUnitDevice;
        }
        [Route("getdatadevice")]
        public JsonResult getDataDevice(string PLC, string Unit)
        {
            var listTagName = _db.Catalog_Datas.Where(x => x.Unit == Unit && x.DeviceName.Contains(PLC));
            var data = (from d in _db.Datas
                        join c in _db.Catalog_Datas on d.TagName equals c.TagName
                        select new { TagName = d.TagName, DeviceName = d.DeviceName, Time = d.Time, Value = d.Value, Unit = c.Unit, Connected = d.Connected })
                             .Where(s => s.DeviceName.Contains(PLC));

            

            List<ListDataDevice> result = new List<ListDataDevice>();
            foreach (var item in listTagName.ToList())
            {

                var x = data.Where(x => x.TagName == item.TagName);

                List<DataDevice> dataDevices = new List<DataDevice>();
                foreach (var y in x.ToList())
                {
                    DataDevice dataDevice = new DataDevice();
                    dataDevice.TagName = y.TagName;
                    dataDevice.DeviceName = y.DeviceName;
                    dataDevice.Time = y.Time;
                    dataDevice.Value = y.Value;
                    dataDevice.Unit = y.Unit;
                    dataDevice.Connected = y.Connected;
                    dataDevices.Add(dataDevice);

                }
                result.Add(new ListDataDevice(item.TagName, dataDevices));
            }
            return Json(result);
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
        public class ListDataDevice
        {
            public string Key { get; set; }
            public List<DataDevice> Value { get; set; }
            public ListDataDevice(string key, List<DataDevice> list)
            {
                Key = key;
                Value = list;
            }
        }
    }
}