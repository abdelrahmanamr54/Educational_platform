using Educational_platform.IRepositery;
using Educational_platform.Models;
using Educational_platform.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Educational_platform.Controllers
{
    public class QuestionController : Controller
    {
        private readonly IQuestionRepository questionRepository;
        public QuestionController(IQuestionRepository questionRepository)
        {
            this.questionRepository = questionRepository;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            var listOfQuestions = questionRepository.ReadAll();
            return View("Index", listOfQuestions);
        }


        [HttpGet]
        public IActionResult ReadById(int id)
        {
            var question = questionRepository.Details().First(e => e.Id == id);
            return View(question);


        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult CreateNew()
        {

            var question = new QuestionVM();

            return View(question);
        }
        [HttpPost]

        public IActionResult SaveNew(QuestionVM questionVM)
        {
            if (ModelState.IsValid)
            {
                Question question = new Question();

                question.QAnswers = questionVM.QAnswers;
                question.ExamId = questionVM.ExamId;
                
                questionRepository.Create(question);

                return RedirectToAction("Index");

            }
            return View("CreateNew", questionVM);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            Question? question = questionRepository.ReadById(id);

            if (question == null)
            {
                return RedirectToAction("Index");
            }

            QuestionVM questionVM = new QuestionVM()
            {
                Id = question.Id,
                QAnswers = question.QAnswers,
                ExamId = question.ExamId,
            };

            return View(questionVM);
        }

        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult SaveEdit(Question question)
        {
            if (question == null)
            {
                return View("Edit", question);
            }

            questionRepository.Update(question);
            return RedirectToAction("Index");
        }


        [Authorize(Roles = "Admin")]

        [HttpGet]
        public IActionResult Delete(int id)
        {
            questionRepository.Delete(id);

            return RedirectToAction("Index");


        }
    }
}
