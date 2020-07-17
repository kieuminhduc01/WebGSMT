using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebGSMT.Areas.Admin.Controllers;
using WebGSMT.Models;

namespace WebGSMT.Areas.Admin.Models.Role
{
    public class RolePaging
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<RoleModel> data { get; set; }
    }

    public class RoleModel
    {
        public string RoleName { get; set; }
        public string Description { get; set; }
        public string Actions { get; set; }

    }
}
