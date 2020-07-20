using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebGSMT.Areas.Users
{
    [Area("Users")]
    [Route("{area}/Devices")]
    public class DevicesController : Controller
    {
        [Route("ListDevice")]
        public IActionResult Index()
        {
            return View();
        }
    }
}