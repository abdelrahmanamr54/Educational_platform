using Educational_platform.IRepositery;
using Educational_platform.Models;
using Educational_platform.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Educational_platform.Controllers
{
    public class ExamController : Controller
    {
        private readonly IExamRepository examRepository;
        public ExamController(IExamRepository examRepository)
        {
            this.examRepository = examRepository;
        }
        public IActionResult Index()
        {
            var listOfExams = examRepository.ReadAll();
            return View("Index", listOfExams);
        }


        [HttpGet]
        public IActionResult ReadById(int id)
        {
            //var exam = examRepository.Details().First(e => e.Id == id);
            //return View(exam);
            var exam = examRepository.GetExamWithAns().First(e => e.Id == id);
            return View(exam);


        }
        [HttpGet]
        public IActionResult GetExamWithAns(int id)
        {
            var exam = examRepository.GetExamWithAns().First(e => e.Id == id);
            return View(exam);


        }


        [HttpGet]
        public IActionResult CreateNew()
        {

            var exam = new ExamVM();
            return View(exam);
        }
        [HttpPost]

        public IActionResult SaveNew(ExamVM examVM)
        {
            if (ModelState.IsValid)
            {
                Exam exam = new Exam();
                exam.Name = examVM.Name;
                exam.Description = examVM.Description;
                exam.QuestionImg = examVM.QuestionImg;
                exam.LectureId = examVM.LectureId;

                examRepository.Create(exam);

                return RedirectToAction("Index");

            }
            return View("CreateNew", examVM);
        }


        public IActionResult Edit(int id)
        {
            Exam? exam = examRepository.ReadById(id);

            if (exam == null)
            {
                return RedirectToAction("Index");
            }

            ExamVM examVM = new ExamVM()
            {
                Id = exam.Id,
                Name = exam.Name,
                Description = exam.Description,
                QuestionImg = exam.QuestionImg,
                LectureId = exam.LectureId,

            };

            return View(examVM);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult SaveEdit(Exam exam)
        {
            if (exam.Name == null)
            {
                return View("Edit", exam);
            }

            examRepository.Update(exam);
            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Delete(int id)
        {
            examRepository.Delete(id);

            return RedirectToAction("Index");


        }
    }
}
