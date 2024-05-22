namespace Educational_platform.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public double Price { get; set; }
        public int GradeId { get; set; }

        public string FilePath { get; set; }

        public Grade grade { get; set; }
    }
}
