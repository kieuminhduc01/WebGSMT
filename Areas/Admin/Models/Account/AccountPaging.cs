using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebGSMT.Areas.Admin.Models.Account
{
    public class AccountPaging
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<AccountShow> data { get; set; }
    }
    public class AccountShow
    {
        public String UserName { get; set; }
        public String FullName { get; set; }
        public DateTime DOB { get; set; }
        public String Email { get; set; }
        public String PhoneNumber { get; set; }
        public bool Active { get; set; }

    }
}
