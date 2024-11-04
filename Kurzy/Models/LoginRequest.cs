﻿namespace Kurzy.Models
{
    public class LoginRequest
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string UserName
        {
            get
            {
                return $"{FirstName} {LastName}";
            }
        }
        public string Password { get; set; } = null!;
    }
}