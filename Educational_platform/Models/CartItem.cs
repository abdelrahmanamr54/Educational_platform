namespace Educational_platform.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public int LectureId { get; set; }
        public string StudentId { get; set; }
        public Lecture Lecture { get; set; }
    }
}
