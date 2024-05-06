using Educational_platform.Data;
using Educational_platform.Models;
using Educational_platform.Repository;
using Educational_platform.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Educational_platform.Controllers
{
    public class ContactusController : Controller
    {

        private readonly ApplicationDbContext context;
        public ContactusController(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult addGrade(int id)
        {

            return View();
        }

        [HttpPost]

        public IActionResult add_Grade(Contactus contactus)
        {

            context.contactus.Add(contactus);
            context.SaveChanges();


           
            return RedirectToAction("Index", "Home");
        }
        public IActionResult showAllContact()
        {
            var contact = context.contactus.ToList();

            return View(contact);
        }
    }
}
