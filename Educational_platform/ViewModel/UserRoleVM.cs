using System.ComponentModel.DataAnnotations;
namespace Educational_platform.ViewModel
{
    public class UserRoleVM
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
