using System.ComponentModel.DataAnnotations;

namespace Educational_platform.ViewModel
{
    public class BookVM
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public string FilePath { get; set; }

        public double Price { get; set; }
        public int GradeId { get; set; }
    }
}
