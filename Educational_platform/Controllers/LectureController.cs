using Educational_platform.Data;
using Educational_platform.IRepositery;
using Educational_platform.Models;
using Educational_platform.ViewModel;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Index()
        {
            var listOfLectures = lectureRepository.ReadAll();
            return View("Index", listOfLectures);
        }


        [HttpGet]
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
                lec.GradeId = lectureVM.GradeId;
                
                lectureRepository.Create(lec);

                return RedirectToAction("Index");

            }
            return View("CreateNew", lectureVM);
        }

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

       


    }
}
