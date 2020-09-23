using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebGSMT.Areas.Admin.Models.Account;
using WebGSMT.Models;
namespace WebGSMT.Service.AccountService
{
    public  class AccountService : Controller
    {
        private Models.GiamSatMoiTruongDbContext _context = new Models.GiamSatMoiTruongDbContext();
        public JsonResult GetAllUser()
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
                        ClassCheck = listAccount[i].Active ? "fa-user-check" : "fa-user-lock"
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
    }
}
