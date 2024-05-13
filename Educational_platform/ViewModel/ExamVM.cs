using Educational_platform.Models;

namespace Educational_platform.ViewModel
{
    public class ExamVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int LectureId { get; set; }
    }
}
