using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebGSMT.Areas.Users.Models.Devices
{

    public class DevicesPaging
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<DevicesModel> data { get; set; }
    }

    public class DevicesModel
    {
        public string Name { get; set; }
        public string NameShow { get; set; }
        public string BranchOrProtocol { get; set; }
        public string Actions { get; set; }
    }

}
