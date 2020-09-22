using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebGSMT.ActionFilter;
using WebGSMT.Areas.Users.Models.Datas;
using WebGSMT.Areas.Users.Models.Devices;
using WebGSMT.Models;

namespace WebGSMT.Areas.Users.Controllers
{
    [Area("Users")]
    [Route("{area}/CatalogData")]
    [Obsolete]
    public class Catalog_DataController : Controller
    {
        private readonly GiamSatMoiTruongDbContext _context;
        private IHostingEnvironment Environment;
        public Catalog_DataController(IHostingEnvironment _environment, GiamSatMoiTruongDbContext context)
        {
            Environment = _environment;
            _context = context;
        }

        // GET: Users/Catalog_Data
        #region Show Catalog Data
        [Route("Index")]
        public IActionResult Index(string DeviceName = "")
        {
            ViewBag.DeviceName = DeviceName;
            return PartialView();
        }
        #endregion

        #region GetAllCatalogData
        [Route("getallcatalogdata")]
        public JsonResult GetAllCatalogData(string name)
        {
            try
            {
                int length = int.Parse(Request.Query["length"]);
                int start = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(int.Parse(Request.Query["start"]) / length))) + 1;
                string searchValue = Request.Query["search[value]"];
                string sortColumnName = Request.Query["columns[" + Request.Query["order[0][column]"] + "][name]"];
                string sortDirection = Request.Query["order[0][dir]"];

                CatalogDataPaging apg = new CatalogDataPaging();
                apg.data = new List<CatalogDataModel>();
                start = (start - 1) * length;
                List<Catalog_Data> listCatalogData = new List<Catalog_Data>();
                if (!string.IsNullOrEmpty(name))
                {
                    listCatalogData = _context.Catalog_Datas.Where(x => x.DeviceName == name).ToList<Catalog_Data>();
                }
                else
                {
                    listCatalogData = _context.Catalog_Datas.ToList<Catalog_Data>();
                }

                apg.recordsTotal = listCatalogData.Count;
                //filter
                if (!string.IsNullOrEmpty(searchValue))
                {
                    listCatalogData = listCatalogData.Where(x => x.DeviceName.ToLower().Contains(searchValue.ToLower())).ToList<Catalog_Data>();
                }
                //sorting
                if (sortDirection == "asc")
                {
                    listCatalogData = listCatalogData.OrderBy(x => x.GetType().GetProperty(sortColumnName).GetValue(x)).ToList<Catalog_Data>();
                }
                else
                {
                    listCatalogData = listCatalogData.OrderByDescending(x => x.GetType().GetProperty(sortColumnName).GetValue(x)).ToList<Catalog_Data>();
                }

                apg.recordsFiltered = listCatalogData.Count;
                //paging
                listCatalogData = listCatalogData.Skip(start).Take(length).ToList<Catalog_Data>();

                foreach (var i in listCatalogData)
                {
                    CatalogDataModel rm = new CatalogDataModel
                    {
                        TagName = i.TagName,
                        DeviceName = i.DeviceName,
                        Address = i.Address,
                        Unit = i.Unit,
                        WarningMax = i.WarnningMax,
                        WarningMin = i.WarnningMin,
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

        #region Create Catalog Data
        // GET: Users/Catalog_Data/Create
        [Route("create")]
        [AuthorizePermission("Danh muc du lieu-Them moi")]
        public IActionResult Create(string DeviceName)
        {
            ViewBag.DeviceName = DeviceName;
            return View();
        }

        [HttpPost]
        [Route("createcatalog")]
        public bool CreateCatalog(string TagName, string deviceName, string Address, string Unit, string WarningMin, string WarningMax)
        {
            try
            {
                Catalog_Data cld = new Catalog_Data()
                {
                    TagName = TagName,
                    DeviceName = deviceName,
                    Address = Address,
                    Unit = Unit,
                    WarnningMin = double.Parse(WarningMin),
                    WarnningMax = double.Parse(WarningMax)
                };
                _context.Add(cld);
                _context.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }
        #endregion

        #region Edit Catalog Data
        // GET: Users/Catalog_Data/Edit/5
        [Route("edit")]
        [HttpPost]
        [AuthorizePermission("Danh muc du lieu-Sua")]
        public IActionResult Edit(string TagName)
        {
            var catalog_Data = _context.Catalog_Datas.Find(TagName);
            return View(catalog_Data);
        }

        [HttpPost]
        [Route("updatecatalogdata")]
        public string UpdateCatalogData(string TagName, string DeviceName, string Address, string Unit, string WarningMin, string WarningMax)
        {

            try
            {

                var d = _context.Catalog_Datas.Find(TagName);
                if (d == null)
                {
                    return "Data not exist";
                }
                d.TagName = TagName;
                d.DeviceName = DeviceName;
                d.Address = Address;
                d.Unit = Unit;
                d.WarnningMin = Convert.ToDouble(WarningMin);
                d.WarnningMax = Convert.ToDouble(WarningMax);

                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return "Edit Fail !!!";
            }
            return "success";
        }
        #endregion

        #region Delete Devices
        [HttpPost]
        [Route("deletecatalog")]
        [AuthorizePermission("Danh muc du lieu-Xoa")]
        public bool DeleteCatalog(string TagName)
        {
            try
            {
                var device = _context.Catalog_Datas.Single(a => a.TagName == TagName);
                _context.Catalog_Datas.Remove(device);
                _context.SaveChanges();
            }
            catch
            {
                return false;
            }
            return true;
        }
        #endregion

        #region Import Data
        [HttpPost]
        [Route("importfile")]
        public string ImportFile(IFormFile postedFiles)
        {
            string wwwPath = this.Environment.WebRootPath;
            string contentPath = this.Environment.ContentRootPath;
            string path = Path.Combine(this.Environment.WebRootPath, "Uploads");
            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                string fileName = Path.GetFileName(postedFiles.FileName);
                using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                {
                    postedFiles.CopyTo(stream);
                }
                string[] lines = System.IO.File.ReadAllLines(path + "/" + fileName);

                for (int i = 0; i < lines.Length; i++)
                {
                    string[] cell = lines[i].Split(',');
                    Catalog_Data cd = new Catalog_Data()
                    {
                        TagName = cell[0].ToString(),
                        DeviceName = cell[1].ToString(),
                        Unit = cell[2].ToString(),
                        Address = cell[3].ToString(),
                        WarnningMin = Convert.ToDouble(cell[4]),
                        WarnningMax = Convert.ToDouble(cell[5])
                    };
                    _context.Catalog_Datas.Add(cd);
                }
                _context.SaveChanges();


                return "success";
            }
            catch (Exception)
            {
                return "Lỗi tải file(chưa chọn file)";
            }

        }
        #endregion
    }
}
