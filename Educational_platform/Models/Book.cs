namespace Educational_platform.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int GradeId { get; set; }
       

        public Grade grade { get; set; }
    }
}
