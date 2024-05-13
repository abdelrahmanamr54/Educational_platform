using Educational_platform.Models;

namespace Educational_platform.ViewModel
{
    public class QuestionVM
    {
        public int Id { get; set; }

        public Answer QAnswers { get; set; }
       
        public int ExamId { get; set; }
    }
}
