using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebGSMT.ActionFilter;

namespace WebGSMT.Areas.Users.Controllers
{
    [Area("Users")]
    [Route("{area}/Information")]
    [ServiceFilter(typeof(AuthorizeActionFilter))]
    public class InformationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}