using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SystemSzwajcarski.Models
{
    public class User
    {
        [Key]
        public int idUser { get; set; }
        [Required]
        public string Login { get; set; }
        [Required]
        public string Name { get; set; }

        public bool Organizer  { get; set ; }
        public bool Player { get; set; }

        [Required]
        public string LastName { get; set; }
        [EmailAddress]
        [Required]
        public string Email { get; set; }

        public string Password { get; set; }

        public User()
        {
            Organizer = false;
            Player = false;
        }
        public User(User user,string password)
        {
            Name = user.Name;
            LastName = user.LastName;
            Login = user.Login;
            Email = user.Email;
            Password = password;
            Organizer = user.Organizer;
            Player = user.Player;
        }
        

    }
}
