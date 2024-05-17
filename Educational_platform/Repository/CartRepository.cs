using Educational_platform.Data;
using Educational_platform.IRepositery;
using Educational_platform.Models;
using Microsoft.CodeAnalysis;

namespace Educational_platform.Repository
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext context;

        public CartRepository(ApplicationDbContext context)
        {
            this.context = context; 
        }

        public void AddToCart(int lecId)
        {
            var findItem = context.lectures.Find(lecId);
            if (findItem != null)
            {
                context.cartItems.Add(new CartItem { LectureId = lecId});
                context.SaveChanges();
            }
        }
    }
}
