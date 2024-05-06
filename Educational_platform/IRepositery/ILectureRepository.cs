using Educational_platform.Data;
using Educational_platform.Models;

namespace Educational_platform.IRepositery
{
    public interface ILectureRepository
    {
        void Delete(int id);
        void Update(Lecture lecture);
        List<Lecture> ReadAll();
        Lecture ReadById(int id);
        List<Lecture> Details();
        void Create(Lecture lecture);

    }
}
