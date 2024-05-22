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


           

            // GET: Enrollment/Index
         
            [HttpPost]
            public async Task<IActionResult> Enroll(int courseId, string code)
            {
                if (string.IsNullOrEmpty(code))
                {
                    ViewBag.Message = "Enrollment code cannot be empty!";
                    return RedirectToAction("AddCart");
                }


                var enrollmentCode = context.enrollmentCodes
                    .FirstOrDefault(e => e.Code == code);

                if (enrollmentCode != null)
                {
            
                var user = await userManager.GetUserAsync(User);
                if (user == null)
                {
                 
                    return RedirectToAction("Login", "Account");
                }
                var lec = await context.lectures.FindAsync(courseId);
                string userlogedgradeId = user.Id;

            
                var existingEnrollment = context.cartItems
                        .FirstOrDefault(e => e.LectureId == courseId && e.StudentId==userlogedgradeId);

                    if (existingEnrollment != null)
                    {
                        ViewBag.Message = "You are already enrolled in this course!";
                    }
                    else
                    {
              

                  
                    var enrollment = new CartItem
                        {
                           StudentId = userlogedgradeId,
                            LectureId = courseId,
                            Lecture=lec
                        };

                    context.cartItems.Add(enrollment);
                        context.SaveChanges();

                        // Mark the enrollment code as used (delete or mark it in some way)
                     context.enrollmentCodes.Remove(enrollmentCode);
                        context.SaveChanges();

                        ViewBag.Message = "Enrolled successfully!";
                    }
                }
                else
                {
                    ViewBag.Message = "Invalid enrollment code or course ID!";
                }

            //return View("index","Lecture");
            return RedirectToAction("index", "Lecture");
        }
        [HttpPost]
        public async Task<IActionResult> EnrollBook(int BookId, string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                ViewBag.Message = "Enrollment code cannot be empty!";
                return RedirectToAction("AddCart");
            }


            var enrollmentCode = context.enrollmentCodes
                .FirstOrDefault(e => e.Code == code);

            if (enrollmentCode != null)
            {

                var user = await userManager.GetUserAsync(User);
                if (user == null)
                {

                    return RedirectToAction("Login", "Account");
                }
                var book = await context.books.FindAsync(BookId);
                string userlogedgradeId = user.Id;


                var existingEnrollment = context.bookCarts
                        .FirstOrDefault(e => e.bookId == BookId && e.StudentId == userlogedgradeId);

                if (existingEnrollment != null)
                {
                    ViewBag.Message = "You are already enrolled in this course!";
                }
                else
                {



                    var enrollment = new BookCart
                    {
                        StudentId = userlogedgradeId,
                      bookId = BookId,
                       book = book
                    };

                    context.bookCarts.Add(enrollment);

                    context.SaveChanges();

                    // Mark the enrollment code as used (delete or mark it in some way)
                    context.enrollmentCodes.Remove(enrollmentCode);
                    context.SaveChanges();

                    ViewBag.Message = "Enrolled successfully!";
                }
            }
            else
            {
                ViewBag.Message = "Invalid enrollment code or course ID!";
            }

            //return View("index","Lecture");
            return RedirectToAction("index", "Lecture");
        }

        // GET: Enrollment/AllCourses



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
        public IActionResult Download(int id )
        {
            var book = context.books.Find(id);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Files", book.FilePath);
            var fileBytes = System.IO.File.ReadAllBytes(filePath);
            var fileName = book.FilePath;
            return File(fileBytes, "application/pdf", fileName);
        }


        [HttpGet]

        public async Task<IActionResult> MyBooks()
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                // return Json(new { success = false, message = "Userr nott logged in." });
                return RedirectToAction("Login", "Account");
            }
            string userlogedgradeId = user.Id;


            var usercart = context.bookCarts.Include(e => e.book).Where(e => e.StudentId == userlogedgradeId).ToList();

            return View(usercart);

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