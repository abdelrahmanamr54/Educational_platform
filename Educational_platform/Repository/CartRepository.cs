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

      
    }
}

