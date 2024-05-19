using System.ComponentModel.DataAnnotations;

namespace Educational_platform.ViewModel
{
    public class LectureVM
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

    //    public double? Price { get; set; }
        public string Content { get; set; }
        [Required]
        public string VideoUrl { get; set; }
        public string ImageUrl { get; set; }
        public int GradeId { get; set; }
    }
}
