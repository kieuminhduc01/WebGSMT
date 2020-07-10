using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebGSMT.Models
{
    public class Account_Role
    {
       
        public String RoleName { get; set; }
        
        public String UserName { get; set; }
       
        public Account Account { get; set; }
        public Role Role { get; set; }
    }
}
