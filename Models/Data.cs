using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebGSMT.Models
{
    public class Data
    {
        
        public string TagName { get; set; }
        [Key]
        public string DeviceName { get; set; }
        public DateTime Time { get; set; }
        public double Value { get; set; }
        public bool Connected { get; set; }
        public Device Device { get; set; }
        public Catalog_Data Catalog_Data { get; set; }
        public string DiemDo { get; set; }
    }
}
