using Microsoft.AspNetCore.Identity;

namespace Educational_platform.Models
{
    public class Student : IdentityUser
    {


        public int GradeId { get; set; }


        public Grade grade { get; set; }

        public string Address { get; set; }
    }
}
