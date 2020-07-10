using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebGSMT.Models
{
    public class Permission_Role
    {
        
        public string RoleName { get; set; }
        
        public int PermissionID { get; set; }
        public Permission Permission { get; set; }
        public Role Role { get; set; }
    }
}
