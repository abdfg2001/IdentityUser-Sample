using IdentityUser_Sample.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityUser_Sample.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly UserManager<IdentityUser> userManager;

        RoleManager<IdentityRole> _RoleManager;
        public HomeController(ILogger<HomeController> logger, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> RoleManager)
        {
            this.userManager = userManager;
            _RoleManager = RoleManager;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(userManager.Users.ToList());
        }

        public async Task<IActionResult> ViewRoles(string Id)
        {
            ViewBag.ID = Id;
            IdentityUser user = await userManager.FindByIdAsync(Id);
            var roles = await userManager.GetRolesAsync(user);
            ViewBag.roles = new SelectList(_RoleManager.Roles.ToList());

            return View("ViewRoles", roles.ToList());
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(string Rol, string Id)
        {
            ViewBag.ID = Id;
            IdentityUser user = await userManager.FindByIdAsync(Id);
            var role = await userManager.GetRolesAsync(user);
            if (!(String.IsNullOrEmpty(Rol)))
            {
                string someRol = role.FirstOrDefault(c => c == Rol);
                if (String.IsNullOrEmpty(someRol))
                {
                    await userManager.AddToRoleAsync(user, Rol);
                }
            }
            var roles = await userManager.GetRolesAsync(user);
            ViewBag.roles = new SelectList(_RoleManager.Roles.ToList());
            return View("ViewRoles", roles.ToList());
        }

        public async Task<IActionResult> DeleteRole(string Id, string Rol)
        {
            ViewBag.ID = Id;
            IdentityUser user = await userManager.FindByIdAsync(Id);
            await userManager.RemoveFromRoleAsync(user, Rol);
            var roles = await userManager.GetRolesAsync(user);
            ViewBag.roles = new SelectList(_RoleManager.Roles.ToList());
            return View("ViewRoles", roles.ToList());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
