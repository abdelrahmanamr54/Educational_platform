using Educational_platform.Models;

namespace Educational_platform.IRepositery
{
    public interface IQuestionRepository
    {
        void Delete(int id);
        void Update(Question question);
        List<Question> ReadAll();
        Question ReadById(int id);
        List<Question> Details();
        void Create(Question question);
    }
}
