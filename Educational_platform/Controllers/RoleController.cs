using Educational_platform.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Educational_platform.Controllers
{
    public class RoleController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        private readonly RoleManager<IdentityRole> role;

        public RoleController(RoleManager<IdentityRole> role)
        {
            this.role = role;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(UserRoleVM userRoleVM)
        {

            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole(userRoleVM.Name);
                var result = await role.CreateAsync(identityRole);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "invalid role");
                }

            }
            return View();
        }
    }
}
