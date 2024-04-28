namespace Educational_platform.Models
{
    public class Question
    {
        public int Id { get; set; }
       
        public Answer QAnswers{ get; set; }
        public Exam exam { get; set; }
        public int ExamId { get; set; }

    }
}
