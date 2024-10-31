using Microsoft.AspNetCore.Identity;
using System.Drawing;

namespace VseTShirts.DB.Models
{
    public class User : IdentityUser   
    {
        public string Role { get; set; }
        public string AvatarURL { get; set; }
    }
}
