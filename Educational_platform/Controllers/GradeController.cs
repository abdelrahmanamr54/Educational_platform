using Educational_platform.Data;
using Educational_platform.IRepositery;
using Educational_platform.Models;
using Educational_platform.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Educational_platform.Controllers
{
    public class GradeController : Controller
    {
        private readonly IGradeRepository gradeRepository;
        private readonly ApplicationDbContext context;
        public GradeController(IGradeRepository gradeRepository,  ApplicationDbContext context)
        {
            this.gradeRepository = gradeRepository;
            this.context = context;

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
        public IActionResult addGrade()
        {
            var Grades = context.grades.ToList();
            ViewBag.Grades = Grades;

            return View();
        }

        [HttpPost]

        public IActionResult add_Grade(GradeVM grade)
        {
            var Grades = context.grades.ToList();
            ViewBag.Grades = Grades;

            var newGrade = new Grade()
            {
                Name= grade.Name
            };

         
            gradeRepository.Create(newGrade);
            return RedirectToAction("Index", "Home");
        }



    }
}
