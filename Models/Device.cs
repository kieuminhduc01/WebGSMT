using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebGSMT.Models
{
    public class Device
    {
       
        public String Name { get; set; }
        public String BranchOrProtocol { get; set; }
        public String NameShow { get; set; }
        public List<Data> Datas { get; set; }

    }
}
