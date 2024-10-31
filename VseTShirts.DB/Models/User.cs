using Microsoft.AspNetCore.Identity;
using System.Drawing;

namespace VseTShirts.DB.Models
{
    public class User : IdentityUser   
    {
        public string Role { get; set; }
        public string AvatarURL { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Status { get; set; }
        public string Gender { get; set; }
        public DateTime DateOfBirth { get;  set; }
    }
}
