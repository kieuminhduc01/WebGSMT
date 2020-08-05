using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebGSMT.Areas.Users.Models.Home
{
    public class DataPaging
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<DataDevice> data { get; set; }
    }

    public class DataDevice
    {
        public string TagName { get; set; }
        public string DeviceName { get; set; }
        public DateTime Time { get; set; }
        public double Value { get; set; }
        public string Unit { get; set; }
        public bool Connected { get; set; }
    }
}
