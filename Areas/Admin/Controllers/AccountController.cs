using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebGSMT.Areas.Admin.Models.Account;
using WebGSMT.Models;

namespace WebGSMT.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Route("{area}/account")]
    public class AccountController : Controller
    {
        private GiamSatMoiTruongDbContext _context;
        [HttpGet]
        public JsonResult getAllUser()
        {
            try
            {
                int length = int.Parse(Request.Query["length"]);
                int start = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(int.Parse(Request.Query["start"]) / length))) + 1;
                string searchValue = Request.Query["search[value]"];
                string sortColumnName = Request.Query["columns[" + Request.Query["order[0][column]"] + "][name]"];
                string sortDirection = Request.Query["order[0][dir]"];

                AccountPaging apg = new AccountPaging();
                apg.data = new List<AccountShow>();
                start = (start - 1) * length;
                List<Account> listAccount = _context.Accounts.ToList<Account>();
                apg.recordsTotal = listAccount.Count;
                //filter
                if (!string.IsNullOrEmpty(searchValue))
                {
                    listAccount = listAccount.Where(x => x.UserName.ToLower().Contains(searchValue.ToLower()) ||
                        x.Email.ToLower().Contains(searchValue.ToLower()) ||
                        x.FullName.ToLower().Contains(searchValue.ToLower()) ||
                        x.PhoneNumber.ToLower().Contains(searchValue.ToLower())
                    ).ToList<Account>();
                }
                //sorting
                
                if (sortDirection == "asc")
                {
                    listAccount = listAccount.OrderBy(x => x.GetType().GetProperty(sortColumnName).GetValue(x)).ToList<Account>();
                }
                else
                {
                    listAccount = listAccount.OrderByDescending(x => x.GetType().GetProperty(sortColumnName).GetValue(x)).ToList<Account>();
                }
                apg.recordsFiltered = listAccount.Count;
                //paging
                listAccount = listAccount.Skip(start).Take(length).ToList<Account>();
                apg.data = new List<AccountShow>();
                for (int i = 0; i < listAccount.Count(); i++)
                {
                    AccountShow acs = new AccountShow
                    {

                        UserName = listAccount[i].UserName,
                        FullName = listAccount[i].FullName,
                        PhoneNumber = listAccount[i].PhoneNumber,
                        Email = listAccount[i].Email,
                        DOB = listAccount[i].DOB,
                        Active = listAccount[i].Active
                    };
                    apg.data.Add(acs);
                }
                apg.draw = int.Parse(Request.Query["draw"]);
                return Json(apg);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public AccountController(GiamSatMoiTruongDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Accounts
        [Route("index")]
        public async Task<IActionResult> Index()
        {
            _context = new GiamSatMoiTruongDbContext();
            return View(await _context.Accounts.ToListAsync());
        }

        // GET: Admin/Accounts/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts
                .FirstOrDefaultAsync(m => m.UserName == id);
            if (account == null)
            {
                return NotFound();
            }

            return View(account);
        }

        // GET: Admin/Accounts/Create
        [Route("create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Accounts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("create")]
        public async Task<IActionResult> Create([Bind("UserName,FullName,DOB,Email,PhoneNumber,Active")] Account account)
        {
            var accTest = await _context.Accounts.FirstOrDefaultAsync(m => m.UserName == account.UserName);
            if (accTest == null)
            {
                account.Password = "123456789";
                if (ModelState.IsValid)
                {
                    _context.Add(account);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            else if (accTest != null)
            {
                ModelState.AddModelError("UserNameExist", "User Name này đã tồn tại");
            }
            return View(account);
        }
        // GET: Admin/Accounts/Edit/5
        [Route("CheckExits")]
        public JsonResult CheckExits(string userdata)
        {
            System.Threading.Thread.Sleep(200);
            var accTest =  _context.Accounts.Where(m => m.UserName == userdata).SingleOrDefault();
            if (accTest != null)
            {
               
                return Json(1);
            }
            return Json(0);
        }
        // GET: Admin/Accounts/Edit/5

        [Route("edit")]
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts.FindAsync(id);
            if (account == null)
            {
                return NotFound();
            }
            return View(account);
        }

        // POST: Admin/Accounts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("edit")]
        public async Task<IActionResult> Edit([Bind("UserName,Password,FullName,DOB,Email,PhoneNumber,Active")] Account account)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(account);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountExists(account.UserName))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(account);
        }

        // GET: Admin/Accounts/Delete/5
        [Route("delete")]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var account = await _context.Accounts.FirstOrDefaultAsync(m => m.UserName == id);
            if (account == null)
            {
                return NotFound();
            }
            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        // POST: Admin/Accounts/Delete/5
        [Route("delete")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var account = await _context.Accounts.FindAsync(id);
            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccountExists(string id)
        {
            return _context.Accounts.Any(e => e.UserName == id);
        }
        [Route("activeOrNot")]
        public async Task<IActionResult> ActiveOrNot(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var account = await _context.Accounts.FirstOrDefaultAsync(m => m.UserName == id);
            if (account == null)
            {
                return NotFound();
            }
            if (account.Active == true)
            {
                account.Active = false;
            }
            else if (account.Active == false)
            {
                account.Active = true;
            }
            _context.Update(account);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [Route("resetPassword")]
        public async Task<IActionResult> resetPassword(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var account = await _context.Accounts.FirstOrDefaultAsync(m => m.UserName == id);
            if (account == null)
            {
                return NotFound();
            }
            account.Password = "123456789";
            _context.Update(account);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
