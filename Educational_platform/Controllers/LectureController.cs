﻿using Educational_platform.Data;
using Educational_platform.IRepositery;
using Educational_platform.Models;
using Educational_platform.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Educational_platform.Controllers
{
    public class LectureController : Controller
    {
        private readonly ILectureRepository lectureRepository;
        private readonly ApplicationDbContext context;
        public LectureController(ILectureRepository lectureRepository, ApplicationDbContext context)
        {
            this.lectureRepository = lectureRepository;
            this.context = context;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            var listOfLectures = lectureRepository.ReadAll();
            return View("Index", listOfLectures);
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult CreateNew()
        {

            var Grades = context.grades.ToList();
            ViewBag.Grades = Grades;


            var lec = new LectureVM();

            return View(lec);
        }
        [HttpPost]
        public IActionResult SaveNew(LectureVM lectureVM)
        {

            var Grades = context.grades.ToList();
            ViewBag.Grades = Grades;
            if (ModelState.IsValid)
            {
                Lecture lec = new Lecture();
                lec.Name = lectureVM.Name;
                lec.Description = lectureVM.Description;
                lec.Content = lectureVM.Content;
                lec.ImageUrl = lectureVM.ImageUrl;
                lec.VideoUrl = lectureVM.VideoUrl;
                lec.Price = lectureVM.Price;
                lec.GradeId = lectureVM.GradeId;
                
                lectureRepository.Create(lec);

                return RedirectToAction("Index");

            }
            return View("CreateNew", lectureVM);
        }

        [HttpGet]
        [Authorize]
        public IActionResult AddToCart(int lecId)
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
            Lecture? lec = lectureRepository.ReadById(lecId);
            if (lec == null)
            {
                return RedirectToAction("Index");
            }

            LectureVM lectureVM = new LectureVM()
            {
                Id = lec.Id,
                Name = lec.Name,
                Description = lec.Description,
                Content = lec.Content,
                ImageUrl = lec.ImageUrl,
                VideoUrl = lec.VideoUrl,
                Price = lec.Price,
                GradeId = lec.GradeId,
            };

            return View(lectureVM);
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Lecture? lec = lectureRepository.ReadById(id);

            if (lec == null)
            {
                return RedirectToAction("Index");
            }

            LectureVM lectureVM = new LectureVM()
            {
                Id = lec.Id,
                Name = lec.Name,
                Description = lec.Description,
                Content = lec.Content,
                ImageUrl = lec.ImageUrl,
                VideoUrl = lec.VideoUrl,
                Price = lec.Price,
                GradeId = lec.GradeId,
            };

            return View(lectureVM);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult SaveEdit(Lecture lecture)
        {
            if (lecture.Name == null)
            {
                return View("Edit", lecture);
            }

            lectureRepository.Update(lecture);
            return RedirectToAction("Index");
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            lectureRepository.Delete(id);

            return RedirectToAction("Index");


        }

        [HttpGet]
        public IActionResult ReadById(int id)
        {
            var lec = lectureRepository.ReadById(id);
            return View(lec);

        }
        [HttpGet]
        public IActionResult FilterLectures(int? gradeId)
        {
            var lectures = context.lectures.AsQueryable();

            if (gradeId.HasValue && gradeId.Value > 0)
            {
                lectures = lectures.Where(l => l.GradeId == gradeId.Value);
            }

            return PartialView("~/Views/Shared/PartialView/_ShowAllLecPartial.cshtml", lectures.ToList());
        }



    }
}
