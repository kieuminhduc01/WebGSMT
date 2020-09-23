using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebGSMT.Models
{
    public class Catalog_Data
    {

        public String TagName { get; set; }
        public String DeviceName { get; set; }
        public String Unit { get; set; }
        public String Address { get; set; }
        public List<Data> Datas { get; set; }

        public string DiemDo { get; set; }
    }
}
