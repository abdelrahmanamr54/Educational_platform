using Educational_platform.Data;
using Educational_platform.IRepositery;
using Educational_platform.Models;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;

namespace Educational_platform.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext context;

        public CartRepository(ApplicationDbContext context)
        {
            this.context = context; 
        }

<<<<<<< HEAD
        //public void AddToCart(int lectureId, string studentId)
        //{
            
        //    var lecture = context.lectures.Find(lectureId);
        //    if (lecture == null)
        //    {
        //        throw new ArgumentException("Lecture with the provided ID does not exist.");
        //    }

            
        //    var existingItem = context.cartItems
        //        .Where(ci => ci.LectureId == lectureId && ci.StudentId == studentId)
        //        .FirstOrDefault();

           
        //    if (existingItem == null)
        //    {
        //        context.cartItems.Add(new CartItem
        //        {
        //            LectureId = lectureId,
        //            StudentId = studentId,
        //            Lecture = lecture
        //        });
        //    }

           
        //    context.SaveChanges();
        //}
=======
        public void AddToCart(int lecId, string studentId)
        {
            var findItem =   context.lectures.Find(lecId);
            if (findItem != null)

            {
                context.cartItems.AddAsync(new CartItem { LectureId = lecId, StudentId=studentId,Lecture=findItem});
                context.SaveChanges();
            }

          //  return findItem;
        }
>>>>>>> 600fdbd881a7429fb7aef6e38f20bd1154227de0
    }
}

