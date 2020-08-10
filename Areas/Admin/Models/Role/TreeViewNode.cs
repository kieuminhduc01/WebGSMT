using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebGSMT.Areas.Admin.Models.Role
{
    public class TreeViewNode
    {
        public int id { get; set; }
        public string text { get; set; }
        public string parent { get; set; }
        public Dictionary<String, bool> state { get; set; }
    }
}
