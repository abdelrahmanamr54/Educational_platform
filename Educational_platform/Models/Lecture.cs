namespace Educational_platform.Models
{
    public class Lecture
    {
        public int Id { get; set; }
        public string Name { get; set; }
     /// <summary>
       public double? Price { get; set; }
     /// </summary>
        public string Description { get; set; }
        public string Content { get; set; }
        public string VideoUrl { get; set; } 
        public string ImageUrl { get; set; }

        public int GradeId { get; set; }
        public int ExamId { get; set; }
        public Exam Exam { get; set; }
        public Grade grade { get; set; }
      

    }
}
