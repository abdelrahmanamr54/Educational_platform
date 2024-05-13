using Educational_platform.Models;

namespace Educational_platform.IRepositery
{
    public interface IBookRepository
    {
        void Delete(int id);
        void Update(Book book);
        List<Book> ReadAll();
        Book ReadById(int id);
        List<Book> Details();
        void Create(Book book);
    }
}
