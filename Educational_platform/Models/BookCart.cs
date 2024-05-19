namespace Educational_platform.Models
{
    public class BookCart
    {
        public int Id { get; set; }
        public int bookId { get; set; }
        public string StudentId { get; set; }
        public Book book { get; set; }
    }
}
