using Educational_platform.Data;
using Educational_platform.IRepositery;
using Educational_platform.Models;
using Educational_platform.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;


namespace Educational_platform.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartRepository cartRepository;
        private readonly UserManager<Student> userManager; private readonly ApplicationDbContext context;


        public CartController(ICartRepository cartRepository, UserManager<Student> userManager, ApplicationDbContext context)
        {
            this.cartRepository = cartRepository;
            this.userManager = userManager;
            this.context = context;
        }
        public IActionResult Index()
        {
            return View();
        }






    }
}