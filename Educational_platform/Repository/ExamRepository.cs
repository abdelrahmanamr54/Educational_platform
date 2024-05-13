using Educational_platform.Data;
using Educational_platform.IRepositery;
using Educational_platform.Models;
using Microsoft.EntityFrameworkCore;

namespace Educational_platform.Repository
{
    public class ExamRepository : IExamRepository
    {
        private readonly ApplicationDbContext context;
        public ExamRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public void Create(Exam exam)
        {
            context.exams.Add(exam);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var exam = context.exams.Find(id);

            if (exam != null)
            {
                context.exams.Remove(exam);
                context.SaveChanges();
            }
        }

        public List<Exam> ReadAll()
        {
            return context.exams.Include(e => e.Lecture).ToList();
        }

        public Exam ReadById(int id)
        {
            return context.exams.Find(id);

        }

        public List<Exam> Details()
        {
            return context.exams.Include(e => e.Lecture).ToList();
        }

        public void Update(Exam exam)
        {
            var e = context.exams.Find(exam.Id);

            if (e != null)
            {
                e.Id = exam.Id;
                e.Name = exam.Name;
                e.Description = exam.Description;
                e.LectureId = exam.LectureId;

                context.SaveChanges();
            }
        }
    }
}
