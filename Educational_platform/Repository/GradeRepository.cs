using Educational_platform.Data;
using Educational_platform.IRepositery;
using Educational_platform.Models;
using Educational_platform.ViewModel;
using Microsoft.EntityFrameworkCore;
using System.Numerics;

namespace Educational_platform.Repository
{
    public class GradeRepository : IGradeRepository
    {
        private readonly ApplicationDbContext context;
        public GradeRepository(ApplicationDbContext context)
        {
            this.context = context;
        }




        public void Create(Grade grade)
        {
            context.grades.Add(grade);
            context.SaveChanges();
        }

        public void Delete(Grade grade)
        {
            context.grades.Remove(grade);
            context.SaveChanges();
        }

        public Grade  FindById(int id)
        {
            return context.grades.Find(id);
        }
        public List<Lecture> GetLecturerById(int LId)
        {


            var Lecture = context.lectures.Where(e => e.GradeId == LId).ToList();

            return Lecture;
        }
        public List<Book> GetBookById(int BId)
        {



            var Book = context.books.Include(e => e.grade).Where(e => e.GradeId == BId).ToList();

            return Book;
        }
        //public List<StudentVM> GetstudentById(int sId)
        //{
        //   // var students = context.studentVMs.Where(e => e.GradeId == sId).ToList();

        //   // return students;
        //}


        public List<Grade> ReadAll()

        {
            return context.grades.ToList();
        }

        public Grade findGrade(int id)
        {
            var oldgrade = context.grades.Find(id);

            return oldgrade;

        }

        public Grade Update(Grade grade)
        {
            var oldgrade = context.grades.Find(grade.Id);
            if (oldgrade != null)
            {
               oldgrade.Name = oldgrade.Name;
              
                context.SaveChanges();

            }
            return oldgrade;
        }
    }
}
