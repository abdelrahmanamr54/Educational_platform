using Microsoft.AspNetCore.Identity;

namespace Educational_platform.Models
{
    public class Student : IdentityUser
    {
     

        public string grade { get; set; }
    }
}
