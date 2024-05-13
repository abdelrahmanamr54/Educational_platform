using Educational_platform.IRepositery;
using Educational_platform.Models;
using Educational_platform.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Educational_platform.Controllers
{
    public class GradeController : Controller
    {
        private readonly IGradeRepository gradeRepository;
        public GradeController(IGradeRepository gradeRepository)
        {
            this.gradeRepository = gradeRepository;

        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult showAllgrades()
        {

            var grade = gradeRepository.ReadAll();
            return View(grade);
        }

        public IActionResult getLecturebygrade( int id )
        {

            var lecturs = gradeRepository.GetLecturerById(id);
            return View(lecturs);
        }

        public IActionResult getBookbyGrade(int id)
        {

            var books = gradeRepository.GetBookById(id);
            return View(books);
        }
        public IActionResult getStudentbyGrade(int id)
        {

            var students = gradeRepository.GetstudentById(id);
            return View(students);
        }

        [HttpGet]
        public IActionResult editGrade(int id)
        {
            var grade = gradeRepository.findGrade(id);
            
            return View(grade);
        }
    

        [HttpPost]

        public IActionResult editGrade(Grade grade)
        {



           
            gradeRepository.Update(grade);
            return RedirectToAction("Index", "Home");
        }
        public IActionResult deleteGrade(Grade grade)
        {
            gradeRepository.Delete(grade);
            return View();
        }
        [HttpGet]
        public IActionResult addGrade(int id)
        {
           
            return View();
        }

        [HttpPost]

        public IActionResult add_Grade(GradeVM grade)
        {

            var newgrade = new Grade()
            {
                Name= grade.Name
            };

         
            gradeRepository.Create(newgrade);
            return RedirectToAction("Index", "Home");
        }



    }
}
