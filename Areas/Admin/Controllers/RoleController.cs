using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebGSMT.Areas.Admin.Models.Role;
using WebGSMT.Models;

namespace WebGSMT.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("{area}/role")]
    public class RoleController : Controller
    {
        private GiamSatMoiTruongDbContext _context = new GiamSatMoiTruongDbContext();

        public RoleController(GiamSatMoiTruongDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Role
        [Route("listrole")]
        public async Task<IActionResult> ListRole()
        {
            _context = new GiamSatMoiTruongDbContext();
            return View(await _context.Roles.ToListAsync());
        }

        // GET: Admin/Role/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = await _context.Roles
                .FirstOrDefaultAsync(m => m.RoleName == id);
            if (role == null)
            {
                return NotFound();
            }

            return View(role);
        }

        // GET: Admin/Role/Create
        [Route("create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Role/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Route("edit")]
        [HttpPost]
        public async Task<IActionResult> Edit(string id)
        {
            var role = await _context.Roles.FindAsync(id);
            return View(role);
        }
        [Route("delete")]
        // GET: Admin/Role/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = await _context.Roles
                .FirstOrDefaultAsync(m => m.RoleName == id);
            if (role == null)
            {
                return NotFound();
            }
            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();
            return RedirectToAction("ListRole");
        }
        // POST: Admin/Role/Delete/5
        [Route("delete")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var role = await _context.Roles.FindAsync(id);
            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoleExists(string id)
        {
            return _context.Roles.Any(e => e.RoleName == id);
        }

        [HttpPost]
        [Route("updaterole")]
        public string UpdateRole(string RoleName, string Description, List<String> PermissionRole)
        {
            
            try
            {
                if (PermissionRole == null)
                {
                    PermissionRole = new List<string>();
                }
                var r = _context.Roles.Find(RoleName);
                if (r == null)
                {
                    return "Role không tồn tại";
                }
                r.Description = Description;
                //remove
                var getPer = _context.AccoPermission_Roles.Where(x => x.RoleName == RoleName);
                foreach(var i in getPer)
                {
                    _context.AccoPermission_Roles.Remove(i);
                }
                //add new
                foreach (var i in PermissionRole)
                {
                    var pe_ro = new Permission_Role()
                    {
                        RoleName = RoleName,
                        PermissionID = int.Parse(i)
                    };
                    _context.AccoPermission_Roles.Add(pe_ro);
                }
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return "Update Role không thành công !!!";
            }
            return "success";
        }

        [HttpPost]
        [Route("insertrole")]
        public string InsertRole(string RoleName, string Description, List<String> PermissionRole)
        {
            try
            {
                if (PermissionRole == null)
                {
                    PermissionRole = new List<string>();
                }
                if (!RoleDAO.checkRoleName(RoleName))
                {
                    return "Role Name đã tồn tại !!!";
                }

                var rs = new Role()
                {
                    RoleName = RoleName,
                    Description = Description,
                };
                _context.Roles.Add(rs);
                _context.SaveChanges();
                var check = _context.Roles.Find(RoleName);
                if(RoleName == null)
                {
                    return "fail";
                }
                foreach (var i in PermissionRole)
                {
                    var pe_ro = new Permission_Role()
                    {
                        RoleName = RoleName,
                        PermissionID = int.Parse(i)
                    };
                    _context.AccoPermission_Roles.Add(pe_ro);
                }
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return "Insert Role không thành công !!!";
            }
            return "success";

        }
        [Route("deleterole")]
        public bool DeleteRole(string RoleName)
        {
            try
            {
                var rs = _context.Roles.Find(RoleName);
                _context.Roles.Remove(rs);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        [HttpGet]
        [Route("getallpermission")]
        public JsonResult GetAllPermission(string RoleName)
        {
            try
            {
                List<int> listPer = new List<int>();
                Role ra = _context.Roles.Find(RoleName);
                List<TreeViewNode> ls = new List<TreeViewNode>();
                if (ra != null)
                {
                    listPer = _context.AccoPermission_Roles.Where(x => x.RoleName == ra.RoleName).Select(x => x.PermissionID).ToList();
                }
                foreach (var i in _context.Permissions)
                {
                    TreeViewNode tvn = new TreeViewNode
                    {
                        id = i.ID.ToString(),
                        parent = i.Parent,
                        text = i.Text,
                        state = new Dictionary<string, bool>()
                    };
                    
                    if (ra != null &&  !string.IsNullOrEmpty(RoleName) && listPer.Contains(int.Parse(tvn.id)))
                    {
                        tvn.state.Add("selected", true);
                    }
                    ls.Add(tvn);
                }

                var duLieu = Newtonsoft.Json.JsonConvert.SerializeObject(ls);

                return Json(ls);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        [Route("permissiontree")]
        public ActionResult PermissionTree(string RoleName)
        {
            if (RoleName == "")
            {
                return PartialView(new Role());
            }
            var rs = _context.Roles.Find(RoleName);
            return PartialView(rs);
        }
        [Route("testtree")]
        public ActionResult TestTree()
        {
            return View();
        }
        
        [Route("getallrole")]
        public JsonResult GetAllRole()
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
                RolePaging apg = new RolePaging();
                apg.data = new List<RoleModel>();
                start = (start - 1) * length;
                List<Role> listRole = _context.Roles.ToList<Role>();
                apg.recordsTotal = listRole.Count;
                //filter
                if (!string.IsNullOrEmpty(searchValue))
                {
                    listRole = listRole.Where(x => x.RoleName.ToLower().Contains(searchValue.ToLower())).ToList<Role>();
                }
                //sorting
                /*if (sortDirection == "asc")
                {
                    listRole = listRole.OrderBy(x => x.GetType().GetProperty(sortColumnName).GetValue(x)).ToList<Role>();
                }
                else
                {
                    listRole = listRole.OrderByDescending(x => x.GetType().GetProperty(sortColumnName).GetValue(x)).ToList<Role>();
                }*/
                

                apg.recordsFiltered = listRole.Count;
                //paging
                listRole = listRole.Skip(start).Take(length).ToList<Role>();

                foreach (var i in listRole)
                {
                    RoleModel rm = new RoleModel
                    {
                        RoleName = i.RoleName,
                        Description = i.Description,
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

        [Route("getrole")]
        public IActionResult getRole(string UserName)
        {
            ViewBag.RoleNameChecked = _context.Account_Roles.Where(x => x.UserName == UserName).Select(x => x.RoleName).ToList();
            return View(_context.Roles.ToList());
        }
    }
}
