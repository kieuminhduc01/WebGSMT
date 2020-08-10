using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebGSMT.Models;

namespace WebGSMT.Areas.Users.Models.Datas
{
    public class CatalogDataPaging
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public List<CatalogDataModel> data { get; set; }
    }

    public class CatalogDataModel
    {
        public string TagName { get; set; }
        public string DeviceName { get; set; }
        public string Unit { get; set; }
        public string Address { get; set; }
        public double WarningMin { get; set; }
        public double WarningMax { get; set; }
        public string Actions { get; set; }
    }
}
