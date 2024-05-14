namespace Educational_platform.Models
{
    public class Exam
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        
        public List<Question> Questions { get; set; }
        public string QuestionImg { get; set; }
        public int LectureId { get; set; }


        public Lecture Lecture { get; set; }
    }
}
