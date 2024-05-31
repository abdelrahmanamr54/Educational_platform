using Educational_platform.Data;
using Educational_platform.IRepositery;
using Educational_platform.Models;
using Educational_platform.Repository;
using Educational_platform.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;

namespace Educational_platform.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookRepository bookRepository;
        private readonly ApplicationDbContext applicationDbContext;
        public BookController(IBookRepository bookRepository, ApplicationDbContext applicationDbContext)
        {
            this.bookRepository = bookRepository;
            this.applicationDbContext = applicationDbContext;
        }


        [Authorize (Roles ="Admin")]
        public IActionResult Index()
        {
            var listOfBooks = bookRepository.ReadAll();
            return View("Index", listOfBooks);
        }


        [HttpGet]
        public IActionResult ReadById(int id)
        {
            var book = bookRepository.Details().First(e => e.Id == id);
            return View(book);


        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult CreateNew()
        {

            var book = new BookVM();
            var Grades = applicationDbContext.grades.ToList();
            ViewBag.Grades = Grades;

            return View(book);
        }
        [HttpPost]

        public IActionResult SaveNew(BookVM bookVM)
        {
            var Grades = applicationDbContext.grades.ToList();
            ViewBag.Grades = Grades;

            if (ModelState.IsValid)
            {
                Book book = new Book();
                book.Name = bookVM.Name;
                book.Price = bookVM.Price;
                book.GradeId = bookVM.GradeId;
                book.ImageUrl = bookVM.ImageUrl;
                book.FilePath = bookVM.FilePath;

                bookRepository.Create(book);

                return RedirectToAction("Index");

            }
            return View("CreateNew", bookVM);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            Book? book = bookRepository.ReadById(id);

            if (book == null)
            {
                return RedirectToAction("Index");
            }

            BookVM bookVM = new BookVM()
            {
                Id = book.Id,
                Name = book.Name,
                Price = book.Price,
                GradeId = book.GradeId,
                ImageUrl = book.ImageUrl,
                FilePath = book.FilePath,
        };

            return View(bookVM);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult SaveEdit(Book book)
        {
            if (book.Name == null)
            {
                return View("Edit", book);
            }

            bookRepository.Update(book);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            bookRepository.Delete(id);

            return RedirectToAction("Index");


        }
        [HttpGet]
        public IActionResult FilterBooks(int? gradeId)
        {
            var books = applicationDbContext.books.AsQueryable();

            if (gradeId.HasValue && gradeId.Value > 0)
            {
                books = books.Where(l => l.GradeId == gradeId.Value);
            }

            return PartialView("~/Views/Shared/PartialView/_ShowAllBooks.cshtml", books.ToList());
        }


        //public IActionResult DownloadPdf(int id)
        //{
        //    var book = bookRepository.ReadById(id);
        //    if (book == null)
        //    {
        //        return NotFound();
        //    }
        //    if (string.IsNullOrEmpty(book.PdfPath))
        //    {
        //        return NotFound(); 
        //    }

        //    var document = new Document(PageSize.A4);


        //    using (var memoryStream = new MemoryStream())
        //    {
        //        PdfWriter.GetInstance(document, memoryStream);


        //        document.Open();


        //        document.Add(new Paragraph(book.Name));
        //        document.Add(new Paragraph(book.Price.ToString()));

        //        document.Close();


        //        return File(memoryStream.ToArray(), "application/pdf", $"{book.Name}.pdf");
        //    }
        //}
    }
}
