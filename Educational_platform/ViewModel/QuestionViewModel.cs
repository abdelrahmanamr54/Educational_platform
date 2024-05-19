using Educational_platform.Models;

namespace Educational_platform.ViewModel
{
    public class QuestionViewModel
    {
        public int Id { get; set; }
       // public string Text { get; set; }
        public Answer? SelectedAnswer { get; set; }
        public List<Answer> Choices { get; set; } = new List<Answer> { Answer.A, Answer.B, Answer.C, Answer.D };
    }
}
