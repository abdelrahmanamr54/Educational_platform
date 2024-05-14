using Educational_platform.Data;
using Educational_platform.IRepositery;
using Educational_platform.Models;
using Educational_platform.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Educational_platform.Controllers
{
    public class GradeController : Controller
    {
        private readonly IGradeRepository gradeRepository;
        private readonly ApplicationDbContext context;
        private readonly UserManager<Student> userManager;
        public GradeController(IGradeRepository gradeRepository,  ApplicationDbContext context, UserManager<Student> userManager)
        {
            this.gradeRepository = gradeRepository;
            this.context = context;
            this.userManager = userManager;

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
        [Authorize]
        public async Task<IActionResult> getLecturebygrade(  )
        {
            
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {

                return RedirectToAction("Login", "Account");
            }

         int usergradeId = user.GradeId; 

            ViewBag.Grades = usergradeId;



            var lectures = gradeRepository.GetLecturerById(usergradeId);
                //await context.lectures
                //.Where(l => l.GradeId == usergradeId)
                //.ToListAsync();
           
            return View(lectures);
        }

        public async Task<IActionResult> getBookbyGrade(int id)
        {

            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {

                return RedirectToAction("Login", "Account");
            }

            int usergradeId = user.GradeId;





            var books = gradeRepository.GetBookById(usergradeId);
            return View(books);
        }
        public IActionResult getStudentbyGrade(int id)
        {

            //var students = gradeRepository.GetstudentById(id);
            return View();
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
