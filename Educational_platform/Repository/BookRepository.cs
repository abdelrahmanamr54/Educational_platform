using Educational_platform.Data;
using Educational_platform.IRepositery;
using Educational_platform.Migrations;
using Educational_platform.Models;
using Educational_platform.ViewModel;
using Microsoft.EntityFrameworkCore;

namespace Educational_platform.Repository
{
    
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext context;
        public BookRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        public void Create(Book book)
        {
            context.books.Add(book);
            context.SaveChanges();
        }

        public void Delete(int id)
        {
            var book = context.books.Find(id);

            if (book != null)
            {
                context.books.Remove(book);
                context.SaveChanges();
            }
        }

        public List<Book> ReadAll()
        {
            return context.books.Include(e => e.grade).ToList();
        }

        public Book ReadById(int id)
        {
            return context.books.Find(id);
            
        }

        public List<Book> Details()
        {
            return context.books.Include(e => e.grade).ToList();
        }

        public void Update(Book book)
        {
            var b = context.books.Find(book.Id);

            if (b != null)
            {
                b.Id = book.Id;
                b.Name = book.Name;
                b.Price = book.Price;
                b.GradeId = book.GradeId;
                context.SaveChanges();
            }
        }
    }
}
