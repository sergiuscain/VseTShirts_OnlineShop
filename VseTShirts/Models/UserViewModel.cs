﻿using VseTShirts.Helpers;

namespace VseTShirts.Models
{
    public class UserViewModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Role { get; set; }
        public string AvatarUrl { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserStatus Status { get; set; }
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; internal set; }
    }
}
