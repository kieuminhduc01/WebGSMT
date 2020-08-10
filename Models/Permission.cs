using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebGSMT.Models
{
    public class Permission
    {
        public int ID { get; set; }
        public string Parent  { get; set; }
        public string Text { get; set; }
        public List<Permission_Role> Permission_Roles { get; set; }
    }
}
