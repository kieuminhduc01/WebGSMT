using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebGSMT.Models
{
    public class Account
    {
        public String UserName { get; set; }
        public String Password { get; set; }
        public String FullName { get; set; }
        public DateTime DOB { get; set; }
        public String Email { get; set; }
        public String PhoneNumber { get; set; }
        public bool Active { get; set; }

        public List<Account_Role> Account_Roles { get; set; }

    }
}
