using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using SystemSzwajcarski.Models.Account;

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
        public UserRegister()
        {

        }
        [Required]
        public string Login { get; set; }
        [Required]
        public string Name { get; set; }

        public Role Roleuser { get; set; }

        [Required]
        public string LastName { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }

    }
}
