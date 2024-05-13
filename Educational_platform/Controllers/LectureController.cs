using Educational_platform.IRepositery;
using Educational_platform.Models;
using Educational_platform.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Educational_platform.Controllers
{
    public class LectureController : Controller
    {
        private readonly ILectureRepository lectureRepository;

        public LectureController(ILectureRepository lectureRepository)
        {
            this.lectureRepository = lectureRepository;
        }
        public IActionResult Index()
        {
            var listOfLectures = lectureRepository.ReadAll();
            return View("Index", listOfLectures);
        }


        [HttpGet]
        public IActionResult CreateNew()
        {

            var lec = new LectureVM();

            return View(lec);
        }
        [HttpPost]
        
        public IActionResult SaveNew(LectureVM lectureVM)
        {
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
            var lec = lectureRepository.Details().First(e => e.Id == id);
            return View(lec);

        }

       


    }
}
