namespace Educational_platform.ViewModel
{
    public class ExamViewModel
    {
        public int ExamId { get; set; }
        public string ExamName { get; set; }
        public List<QuestionViewModel> Questions { get; set; }
    }
}
