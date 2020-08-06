using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebGSMT.ActionFilter;
using WebGSMT.Areas.Users.Models.Devices;
using WebGSMT.Models;

namespace WebGSMT.Areas.Users
{


    [Area("Users")]
    [Route("{area}/Devices")]
    public class DevicesController : Controller
    {
        private GiamSatMoiTruongDbContext _context = new GiamSatMoiTruongDbContext();
        public DevicesController(GiamSatMoiTruongDbContext context)
        {
            _context = context;

        }
        #region List devices
        [Route("ListDevice")]
        [AuthorizePermission("Thiet bi-Xem")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Devices.ToListAsync());
        }

        [Route("GetAllDevices")]
        public JsonResult GetAllDevices()
        {
            try
            {
                /*db.Configuration.ProxyCreationEnabled = false;*/
                int length = int.Parse(Request.Query["length"]);
                int start = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(int.Parse(Request.Query["start"]) / length))) + 1;
                string searchValue = Request.Query["search[value]"];
                string sortColumnName = Request.Query["columns[" + Request.Query["order[0][column]"] + "][name]"];
                string sortDirection = Request.Query["order[0][dir]"];

                /*int length = 10;
                int start = 1;
                string searchValue = "";
                string sortColumnName = "ID";
                string sortDirection = "asc";*/
                DevicesPaging apg = new DevicesPaging();
                apg.data = new List<DevicesModel>();
                start = (start - 1) * length;
                List<Device> listDevice = _context.Devices.ToList<Device>();
                apg.recordsTotal = listDevice.Count;
                //filter
                if (!string.IsNullOrEmpty(searchValue))
                {
                    listDevice = listDevice.Where(x => x.Name.ToLower().Contains(searchValue.ToLower())).ToList<Device>();
                }


                apg.recordsFiltered = listDevice.Count;
                //paging
                listDevice = listDevice.Skip(start).Take(length).ToList<Device>();

                foreach (var i in listDevice)
                {
                    DevicesModel rm = new DevicesModel
                    {
                        Name = i.Name,
                        NameShow = i.NameShow,
                        BranchOrProtocol = i.BranchOrProtocol,
                        Actions = ""
                    };
                    apg.data.Add(rm);
                }

                apg.draw = int.Parse(Request.Query["draw"]);
                return Json(apg);
                /*return Json(apg, JsonRequestBehavior.AllowGet);*/
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion

        #region Edit devices

        [Route("edit")]
        [HttpPost]
        [AuthorizePermission("Thiet bi-Sua")]
        public async Task<IActionResult> Edit(string id)
        {
            var device = await _context.Devices.FindAsync(id);
            return View(device);
        }
        [HttpPost]
        [Route("updateDevices")]
        public string UpdateDeivces(string name, string nameShow, string branchOrProtocol)
        {

            try
            {

                var d = _context.Devices.Find(name);
                if (d == null)
                {
                    return "device không tồn tại";
                }
                d.NameShow = nameShow;
                d.Name = name;
                d.BranchOrProtocol = branchOrProtocol;
                //remove

                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return "Update device không thành công !!!";
            }
            return "success";
        }
        #endregion

        #region Delete Devices
        [HttpPost]
        [Route("DeleteDevices")]
        public bool DeleteDevices(string name)
        {
            try
            {
                var device = _context.Devices.Single(a => a.Name == name);
                _context.Devices.Remove(device);
                _context.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }
        #endregion

        #region Create devices
        [HttpGet]
        [Route("Create")]
        public IActionResult CreateDevices()
        {
            return View();
        }
        [HttpPost]
        [Route("Create")]
        public bool CreateDevices(string name, string nameShow, string branchOrProtocol)
        {
            try
            {
                Device device = new Device()
                {
                    Name = name,
                    NameShow = nameShow,
                    BranchOrProtocol = branchOrProtocol
                };
                _context.Add(device);
                _context.SaveChangesAsync();
            }
            catch
            {
                return false;
            }
            return true;
        }
        [Route("CheckExist")]
        public JsonResult CheckExist(string deviceName)
        {
            System.Threading.Thread.Sleep(200);
            var accTest = _context.Devices.Where(m => m.Name == deviceName).SingleOrDefault();
            if (accTest != null)
            {

                return Json(1);
            }
            return Json(0);
        }
        #endregion

       
    }
}
