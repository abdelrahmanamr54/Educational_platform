using Educational_platform.IRepositery;
using Educational_platform.Models;
using Educational_platform.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Educational_platform.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartRepository cartRepository;
        private readonly UserManager<Student> userManager;
        public CartController(ICartRepository cartRepository)
        {
            this.cartRepository = cartRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        //public IActionResult AddToCart(int lecId)
        //{
            
        //    string currentStudentId = GetCurrentStudentId();

        //    try
        //    {
        //        cartRepository.AddToCart(lecId, currentStudentId);
        //        return View("Success", new { message = "Lecture added to cart!" });
        //    }
        //    catch (ArgumentException ex)
        //    {
        //        ModelState.AddModelError("", ex.Message); // Add error message from exception
        //        return View("Error"); // Replace with your error view
        //    }
        //}

        
        //private string GetCurrentStudentId()
        //{
        //    if (User.Identity.IsAuthenticated)
        //    {
        //        // Get username from Identity (replace with student ID retrieval if needed)
        //        string studentId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        //        return studentId; 
        //    }
        //    else
        //    {
                
        //        return null;  
        //    }
        //}
    

}
}
