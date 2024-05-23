using Educational_platform.Data;
using Educational_platform.IRepositery;
using Educational_platform.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Educational_platform.Repository
{
    public class LectureRepository : ILectureRepository
    {
        private readonly ApplicationDbContext context;
 
        public LectureRepository(ApplicationDbContext context )
        {
            this.context = context;
 
        }


        public void Create(Lecture lecture)
        {
            context.lectures.Add(lecture);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var lec = context.lectures.Find(id);

            if (lec != null)
            {
                context.lectures.Remove(lec);
                context.SaveChanges();
            }
        }

        public List<Lecture> Details()
        {
            return context.lectures.Include(e=>e.grade).ToList();
        }

        public List<Lecture> ReadAll()
        {
            return context.lectures.Include(e=>e.grade).ToList();
        }

        public Lecture ReadById(int id)
        {
            return context.lectures.Find(id);
        }


        public void Update(Lecture lecture)
        {
            var lec = context.lectures.Find(lecture.Id);

            if (lec != null)
            {
                lec.Id = lecture.Id;
                lec.Name = lecture.Name;
                lec.Description = lecture.Description;
                lec.Content = lecture.Content;
                lec.VideoUrl = lecture.VideoUrl;
                lec.ImageUrl = lecture.ImageUrl;
               // lec.Price = lecture.Price;
                lec.GradeId = lecture.GradeId;
                context.SaveChanges();
            }
        }
    }
}
