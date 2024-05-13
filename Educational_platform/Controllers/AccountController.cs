using Educational_platform.Data;
using Educational_platform.Models;
using Educational_platform.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Educational_platform.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext context;
        UserManager<Student> userManager;
        SignInManager<Student> signIn;
        public AccountController(UserManager<Student> userManager, SignInManager<Student> signIn, ApplicationDbContext context)
        {
            this.userManager = userManager;
            this.signIn = signIn;
            this.context = context;
        }




        [HttpGet]
        public IActionResult Registration()
        {
            var Grades = context.grades.ToList();
            ViewBag.Grades = Grades;
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registration(StudentVM userVM)
        {
            var Grades = context.grades.ToList();
            ViewBag.Grades = Grades;
            if (ModelState.IsValid)
            {
                Student user = new Student()
                {
                    UserName = userVM.Name,
                    Email = userVM.Email
                    ,
                    PasswordHash = userVM.Password,
                    GradeId=userVM.GradeId,
                    Address=userVM.Address
                };

                // var result = await userManager.CreateAsync(user, userVM.Password);
                var result = await userManager.CreateAsync(user
                    , userVM.Password);
                if (result.Succeeded)
                {
                    await signIn.SignInAsync(user, false);
                    return RedirectToAction("Index", "Home");
                }
                return View();
            }
         
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserLoginVM userVM)
        {
            if (ModelState.IsValid)
            {
                var result = await userManager.FindByNameAsync(userVM.UserName);
                if (result != null)
                {
                    bool check = await userManager.CheckPasswordAsync(result, userVM.Password);

                    if (check)
                    {
                        await signIn.SignInAsync(result, userVM.RememberMe);
                        return RedirectToAction("Index", "Home");
                    }
                    ModelState.AddModelError("invalidpw", "invalidpassword");
                }
                else
                {
                    ModelState.AddModelError("invalidU", "invalidusername");
                }

            }
            return View(userVM);

        }
        public async Task<IActionResult> LogOut()
        {
            await signIn.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
