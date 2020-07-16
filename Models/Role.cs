using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebGSMT.Models
{
    public class Role
    {
        public string RoleName { get; set; }
        public string Description { get; set; }
        public List<Account_Role> Account_Roles { get; set; }
        public List<Permission_Role> Permission_Roles { get; set; }
    }
}
