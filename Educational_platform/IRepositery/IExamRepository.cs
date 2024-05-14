using Educational_platform.Models;

namespace Educational_platform.IRepositery
{
    public interface IExamRepository
    {
        void Delete(int id);
        void Update(Exam exam);
        List<Exam> ReadAll();
        Exam ReadById(int id);
        List<Exam> Details();
        void Create(Exam exam);
        List<Exam> GetExamWithAns();
    }
}
