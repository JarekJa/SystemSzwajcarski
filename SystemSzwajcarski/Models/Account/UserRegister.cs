using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SystemSzwajcarski.Models
{
    public class UserRegister
    {
        public UserRegister(User user)
        {
            Name = user.Name;
            LastName = user.LastName;
            Login = user.Login;
            Email = user.Email;
            Password = "Pa$$w0rd";
        }
        [Required]
        public string Login { get; set; }
        [Required]
        public string Name { get; set; }

        public bool Organizer { get; set; }
        public bool Player { get; set; }

        [Required]
        public string LastName { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

        public UserRegister()
        {
            Organizer = false;
            Player = false;
        }
    }
}
