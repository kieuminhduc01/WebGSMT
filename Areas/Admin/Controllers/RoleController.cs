using System;
using System.Collections.Generic;
using System.Linq;
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
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Role/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoleName,Description")] Role role)
        {
            if (ModelState.IsValid)
            {
                _context.Add(role);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(role);
        }
/*
        // GET: Admin/Role/Edit/5
        [Route("edit")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = await _context.Roles.FindAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            return View(role);
        }*/

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
        public string UpdateRole(string RoleName)
        {
            try
            {
                
                var rs = _context.Roles.Find(RoleName);
                if (rs == null)
                {
                    return "Role không tồn tại !!!";
                }
                if (!RoleDAO.checkRoleName(RoleName))
                {
                    return "Role Name đã tồn tại !!!";
                }

                rs.RoleName = RoleName;
                
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                return "Update Role không thành công !!!";
            }
            return "success";
        }
    }
}
