﻿using Educational_platform.Data;
using Educational_platform.IRepositery;
using Educational_platform.Models;
using Educational_platform.Repository;
using Educational_platform.ViewModel;
using Microsoft.AspNetCore.Mvc;

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

                bookRepository.Create(book);

                return RedirectToAction("Index");

            }
            return View("CreateNew", bookVM);
        }


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


        [HttpGet]
        public IActionResult Delete(int id)
        {
            bookRepository.Delete(id);

            return RedirectToAction("Index");


        }
    }
}