
﻿using Educational_platform.Data;
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


        private readonly UserManager<Student> userManager;
        private readonly ApplicationDbContext context;


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


        [HttpGet]

        public IActionResult AddToCart(int lecId)
        {





            var findItem = context.lectures.Find(lecId);

            //if (findItem == null)
            //{
            //    return NotFound("Lecture not found");
            //}

            return View(findItem);
        }




        [HttpPost]

        public async Task<IActionResult> Add_To_Cart(int id)
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                // return Json(new { success = false, message = "Userr nott logged in." });
                return RedirectToAction("Login", "Account");
            }
            string userlogedgradeId = user.Id;

            var lec = await context.lectures.FindAsync(id);

            if (lec == null)
            {
                return NotFound("Lecture not found");
            }


            context.cartItems.AddAsync(new CartItem { LectureId = lec.Id, StudentId = userlogedgradeId, Lecture = lec });
            context.SaveChanges();
            return RedirectToAction("Index", "home");
            // return View(cart);

        }


    }
}
// var lec= await cartRepository.AddToCart(lecId,userlogedgradeId);
//if (lec)
//{
//    return RedirectToAction("Index", "Home");
//}
//   cartRepository.AddToCart(id, userlogedgradeId);
//if (addedToCart)
//{
//    var response = JsonConvert.SerializeObject(new { message = "Product added to cart", productId = productId });
//}
// return Json(new { success = true, message = "Lecture added to cart!" });
// var findItem = await context.lectures.FindAsync(lec.Id);
//var cart = new CartItem()
//{
//    LectureId = findItem.Id,
//    StudentId = userlogedgradeId,
//    Lecture = findItem
//};