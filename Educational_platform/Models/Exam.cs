namespace Educational_platform.Models
{
    public class Exam
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        //public Answer Q1 { get; set; }
        //public Answer Q2 { get; set; }
        //public Answer Q3 { get; set; }
        //public Answer Q4 { get; set; }
        //public Answer Q5 { get; set; }
        //public Answer Q6{ get; set; }
        //public Answer Q7 { get; set; }
        //public Answer Q8 { get; set; }
        //public Answer Q9 { get; set; }
        //public Answer Q10 { get; set; }
        //public int GradeId { get; set; }


        //public Grade grade { get; set; }

        public List<Question> questions { get; set; }
        public int LectureId { get; set; }


        public Lecture Lecture { get; set; }
    }
}
