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
        private readonly UserManager<Student> userManager;
        private readonly ApplicationDbContext context;
        private readonly ILectureRepository lectureRepository;

        public CartController(ICartRepository cartRepository, UserManager<Student> userManager, ApplicationDbContext context, ILectureRepository lectureRepository)
        {
            this.cartRepository = cartRepository;
            this.userManager = userManager;
            this.context = context;
            this.lectureRepository = lectureRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]

        public IActionResult AddBookCart(int id)
        {
         Book? book = context.books.Find(id);
          


           BookVM bookVM = new BookVM()
            {
                Id = book.Id,
                Name = book.Name,
               Price= book.Price,
              
                ImageUrl = book.ImageUrl,
           
              
            };
            return View(bookVM);
        }

        [HttpGet]

        public IActionResult AddCart(int id)
        {
            Lecture? lec = context.lectures.Find(id);
            //if (lec == null)
            //{
            //    return RedirectToAction("Index");
            //}


            LectureVM lectureVM = new LectureVM()
            {
                Id = lec.Id,
                Name = lec.Name,
                Description = lec.Description,
                Content = lec.Content,
                ImageUrl = lec.ImageUrl,
                VideoUrl = lec.VideoUrl,
                GradeId = lec.GradeId,
            };
            return View(lectureVM);
        }

        [HttpGet]

        public async Task<IActionResult>  MyCourses()
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                // return Json(new { success = false, message = "Userr nott logged in." });
                return RedirectToAction("Login", "Account");
            }
            string userlogedgradeId = user.Id;


            var usercart = context.cartItems.Include(e=>e.Lecture).Where(e => e.StudentId == userlogedgradeId).ToList();

            return View(usercart);

        }


            [HttpGet]
     
        public  IActionResult AddToCart(int lecId)
        {




            //    var lec = context.lectures.Find(lecId
            //    );


            //    LectureVM lectureVM = new LectureVM()
            //    {
            //        Id = lec.Id,
            //        Name = lec.Name,
            //        Description = lec.Description,
            //        Content = lec.Content,
            //        ImageUrl = lec.ImageUrl,
            //        VideoUrl = lec.VideoUrl,
            //        GradeId = lec.GradeId,
            //    };

            //    if (findItem == null)
            //    {
            //        return NotFound("Lecture not found");
            //    }

            //    return View(lectureVM);
            //Lecture? lec = lectureRepository.ReadById(lecId);
            ////if (lec == null)
            ////{
            ////    return RedirectToAction("Index");
            ////}


            //LectureVM lectureVM = new LectureVM()
            //{
            //    Id = lec.Id,
            //    Name = lec.Name,
            //    Description = lec.Description,
            //    Content = lec.Content,
            //    ImageUrl = lec.ImageUrl,
            //    VideoUrl = lec.VideoUrl,
            //    GradeId = lec.GradeId,
            //};

            return View();
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

            var lec = await context.lectures.FindAsync(id );

            if (lec == null)
            {
                return NotFound("Lecture not found");
            }


            context.cartItems.AddAsync(new CartItem { LectureId = lec.Id, StudentId = userlogedgradeId ,Lecture=lec});
            context.SaveChanges();
            return RedirectToAction("Index","home");
            // return View(cart);

        }
        [HttpPost]

        public async Task<IActionResult> Add_Book_To_Cart(int id)
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                // return Json(new { success = false, message = "Userr nott logged in." });
                return RedirectToAction("Login", "Account");
            }
            string userlogedgradeId = user.Id;

            var book = await context.books.FindAsync(id);

            if (book == null)
            {
                return NotFound("Book not found");
            }


            object value = context.bookCarts.AddAsync(new BookCart {bookId= book.Id, StudentId = userlogedgradeId, book = book});
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