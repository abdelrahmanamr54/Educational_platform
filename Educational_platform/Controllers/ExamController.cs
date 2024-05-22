using Educational_platform.Data;
using Educational_platform.IRepositery;
using Educational_platform.Models;
using Educational_platform.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Educational_platform.Controllers
{
    public class ExamController : Controller
    {
        private readonly IExamRepository examRepository;
        private readonly ApplicationDbContext context;
        public ExamController(IExamRepository examRepository, ApplicationDbContext context)
        {
            this.examRepository = examRepository;
            this.context = context;
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
        public IActionResult TakeExam(int id)
        {
            var exam = context.exams
                .Include(e => e.Questions)
                .FirstOrDefault(e => e.Id == id);

            if (exam == null)
            {
                return NotFound();
            }

            var viewModel = new ExamViewModel
            {
                ExamId = exam.Id,
                ExamName = exam.Name,
                QuestionImg = exam.QuestionImg,
                Questions = exam.Questions.Select(q => new QuestionViewModel
                {
                    Id = q.Id,
                   // Text = q.Text
                }).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult SubmitExam(ExamViewModel model)
        {
         //   if (ModelState.IsValid)
          //  {
                int score = 0;
            int totalQuestions = 10;
                //model.Questions.Count;

                foreach (var question in model.Questions)
                {
                    var correctAnswer = context.questions
                        .Where(q => q.Id == question.Id)
                        .Select(q => q.QAnswers)
                        .FirstOrDefault();

                    if (correctAnswer == question.SelectedAnswer)
                    {
                        score++;
                    }
                }

                var resultViewModel = new ExamResultViewModel
                {
                    ExamId = model.ExamId,
                    ExamName = model.ExamName,
                    Score = score,
                    TotalQuestions = totalQuestions
                };

                return RedirectToAction("ExamResult", resultViewModel);
          //  }
         //   return RedirectToAction("ExamResult", resultViewModel);
            //  return View(model);
        }

        public IActionResult ExamResult(ExamResultViewModel model)
        {
            return View(model);
        }


    }
}
