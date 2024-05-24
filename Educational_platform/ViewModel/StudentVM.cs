using Educational_platform.Models;
using System.ComponentModel.DataAnnotations;

namespace Educational_platform.ViewModel
{
    public class StudentVM
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
       [DataType(DataType.Password)]
        public string Password { get; set; }
       [DataType(DataType.Password)]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }
        public int GradeId { get; set; }
        



      // public Grade grade { get; set; }
        public string Address { get; set; }

    }
}
