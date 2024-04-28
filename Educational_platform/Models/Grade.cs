namespace Educational_platform.Models
{
    public class Grade
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int GradeId { get; set; }


        public List<Book> Books
        { get; set; }
        public List<Student> students
        { get; set; }
        public List<Lecture> lectures
        { get; set; }
    }
}
