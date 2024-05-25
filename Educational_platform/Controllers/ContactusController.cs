using Educational_platform.Data;
using Educational_platform.IRepositery;
using Educational_platform.Models;
using Educational_platform.Repository;
using Educational_platform.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Educational_platform.Controllers
{
    public class ContactusController : Controller
    {

        private readonly IContactusRepositery contactusRepositery;
        public ContactusController(IContactusRepositery contactusRepositery)
        {
            this.contactusRepositery = contactusRepositery;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult addcontact()
        {

            return View();
        }

        [HttpPost]

        public IActionResult add_contact(Contactus contactus)
        {

            contactusRepositery.AddContact(contactus);


           
            return RedirectToAction("Index", "Home");
        }
        [Authorize(Roles = "Admin")]
        public IActionResult showAllContact()
        {
            var contact = contactusRepositery.getAllMsg();

            return View(contact);
        }
        [HttpGet]
        public IActionResult Edit( int id)
        {

            var contact = contactusRepositery.findContact(id);
            return View(contact);
        }
        [HttpPost]
        public IActionResult SaveEdit(Contactus contactus)
        {
         
           contactusRepositery.EditContact(contactus);

       

            return RedirectToAction("Index","Home");
        }
    }
}
