using Educational_platform.Models;
using Educational_platform.ViewModel;

namespace Educational_platform.IRepositery
{
    public interface IGradeRepository
    {
        void Create(Grade grade);

        void Delete(Grade grade);

        Grade findGrade(int id);
        Grade FindById(int id);
      
        List<Lecture> GetLecturerById(int LId);
       
        List<Book> GetBookById(int BId);

        //  List<StudentVM> GetstudentById(int sId);
        

        List<Grade> ReadAll();

       

         Grade Update(Grade grade);
       
    }
}
