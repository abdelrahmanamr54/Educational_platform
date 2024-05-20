using Educational_platform.Data;
using Educational_platform.IRepositery;
using Educational_platform.Models;
using Microsoft.EntityFrameworkCore;

namespace Educational_platform.Repository
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly ApplicationDbContext context;
        public QuestionRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public void Create(Question question)
        {
            context.questions.Add(question);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var question = context.questions.Find(id);

            if (question != null)
            {
                context.questions.Remove(question);
                context.SaveChanges();
            }
        }

        public List<Question> ReadAll()
        {
            return context.questions.Include(e => e.exam).ToList();
        }

        public Question ReadById(int id)
        {
            return context.questions.Find(id);

        }

        public List<Question> Details()
        {
            return context.questions.Include(e => e.exam).ToList();
        }

        public void Update(Question question)
        {
            var q = context.questions.Find(question.Id);

            if (q != null)
            {
                q.Id = question.Id;
                q.QAnswers = question.QAnswers;
               
                q.ExamId = question.ExamId;
                context.SaveChanges();
            }
        }
    }
}
