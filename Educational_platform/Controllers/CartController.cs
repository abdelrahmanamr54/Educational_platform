using Educational_platform.IRepositery;
using Microsoft.AspNetCore.Mvc;

namespace Educational_platform.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartRepository cartRepository;
        public CartController(ICartRepository cartRepository)
        {
            this.cartRepository = cartRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddToCart(int lecId)
        {
            cartRepository.AddToCart(lecId);

            return View("Success", new { message = "Lecture added to cart!" });
        }

    }
}
