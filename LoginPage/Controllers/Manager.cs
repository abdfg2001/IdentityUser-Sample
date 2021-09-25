using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginPage.Controllers
{
    [Authorize(Roles = "Manager")]
    public class Manager : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
