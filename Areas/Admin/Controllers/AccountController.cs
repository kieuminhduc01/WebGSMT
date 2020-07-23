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
        private GiamSatMoiTruongDbContext _context = new GiamSatMoiTruongDbContext();

        public AccountController(GiamSatMoiTruongDbContext context)
        {
            _context = context;
        }

        [Route("getalluser")]
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
                        Active = listAccount[i].Active,
                        Role = string.Join(",", _context.Account_Roles.Where(x => x.UserName == listAccount[i].UserName).Select(x => x.RoleName).ToList()),
                        ClassCheck = listAccount[i].Active?"fa-user-check" : "fa-user-lock"
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
        
        [Route("listuser")]
        public async Task<IActionResult> ListUser()
        {
            _context = new GiamSatMoiTruongDbContext();
            return View(await _context.Accounts.ToListAsync());
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

        [HttpPost]
        [Route("create")]
        public string Create(string UserName, string FullName, string DOB, string Email, string PhoneNumber, string Active, List<String> RoleName)
        {
 
            var accTest = _context.Accounts.Where(m => m.UserName == UserName).SingleOrDefault();
            if (accTest == null)
            {
                if (ModelState.IsValid)
                {
                    try
                    {
                        Account account = new Account()
                        {
                            UserName = UserName,
                            Password = "123456789",
                            FullName = FullName,
                            DOB = DateTime.ParseExact(DOB, "dd/MM/yyyy", null),
                            Email = Email,
                            PhoneNumber = PhoneNumber,
                            Active = Convert.ToBoolean(Active),
                        };
                        _context.Accounts.Add(account);
                        foreach (var i in RoleName)
                        {
                            Account_Role ar = new Account_Role()
                            {
                                RoleName = i,
                                UserName = UserName
                            };
                            _context.Account_Roles.Add(ar);
                        }
                        _context.SaveChanges();
                        return "success";
                    }catch(DbUpdateConcurrencyException)
                    {
                        return "";
                    }
                    
                }
            }
            else if (accTest != null)
            {
                ModelState.AddModelError("UserNameExist", "User Name này đã tồn tại");
            }
            return "fail";
        }
        // GET: Admin/Accounts/Edit/5
        [Route("CheckExits")]
        public JsonResult CheckExits(string userdata)
        {
            System.Threading.Thread.Sleep(200);
            var accTest =  _context.Accounts.Where(m => m.UserName == userdata).SingleOrDefault();
            if (accTest != null )
            {
                if (accTest.ToString().Length != 0)
                {
                    
                    return Json(1);
                    
                }
                else
                    return Json(0);
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
        [Route("edit")]
        public string Edit(string UserName,string Password,string FullName,string DOB,string Email,string PhoneNumber,string Active, List<String> RoleName)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    var account = _context.Accounts.Find(UserName);
                    if (account != null){
                        account.UserName = UserName;
                        account.Password = Password;
                        account.FullName = FullName;
                        account.DOB = DateTime.ParseExact(DOB,"dd/MM/yyyy",null);
                        account.Email = Email;
                        account.PhoneNumber = PhoneNumber;
                        account.Active = Convert.ToBoolean(Active);
                    }
                    foreach(var i in _context.Account_Roles)
                    {
                        if(i.UserName == UserName)
                        {
                            _context.Remove(i);
                        }
                    }
                    foreach (var i in RoleName)
                    {
                        Account_Role ar = new Account_Role()
                        {
                            RoleName = i,
                            UserName = UserName
                        };
                        _context.Account_Roles.Add(ar);
                    }
                    
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccountExists(UserName))
                    {
                        return "Account not exist";
                    }
                    else
                    {
                        return "";
                    }
                }
                return "success";
            }
            return "fail";
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

            return RedirectToAction("listuser");
        }

        // POST: Admin/Accounts/Delete/5
        [Route("delete")]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var account = await _context.Accounts.FindAsync(id);
            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();
            return RedirectToAction("listuser");
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
            return RedirectToAction("ListUser");
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

        [HttpPost]
        [Route("deleteaccount")]
        public bool DeleteAccount(string UserName)
        {
            try
            {
                var rs = _context.Accounts.Find(UserName);
                _context.Accounts.Remove(rs);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }


    }
}
