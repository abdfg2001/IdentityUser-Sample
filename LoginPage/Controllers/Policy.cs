using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoginPage.Controllers
{
    [Authorize(Policy = "CanAccessVIPArea")]
    public class Policy : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
