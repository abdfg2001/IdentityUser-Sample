using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace IdentityUser_Sample.Controllers
{
    public class Claims : Controller
    {
        private readonly UserManager<IdentityUser> userManager;

        public Claims(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }
        public IActionResult Index()
        {
            return View(userManager.Users.ToList());
        }

        public async Task<IActionResult> ViewClaims(string Id)
        {
            IdentityUser user = await userManager.FindByIdAsync(Id);
            var claims = await userManager.GetClaimsAsync(user);
            ViewBag.Claims = claims;
            return View("ViewClaims", Id);
        }

        [HttpPost]
        public async Task<IActionResult> Index(string Tipo, string Id)
        {
            IdentityUser user = await userManager.FindByIdAsync(Id);
            var role = await userManager.GetRolesAsync(user);
            string x = role.First();
            var result = await userManager.AddClaimAsync(user, new Claim(Tipo, x, ClaimValueTypes.String));
            return Redirect("/Claims/ViewClaims/" + Id);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(string Id, string Type, string Value)
        {
            IdentityUser user = await userManager.FindByIdAsync(Id);
            var claims = await userManager.GetClaimsAsync(user);
            Claim x = claims.FirstOrDefault(c => c.Type == Type);
            var a = await userManager.RemoveClaimAsync(user, x);
            return Redirect("/Claims/ViewClaims/" + Id);
        }
    }
}
