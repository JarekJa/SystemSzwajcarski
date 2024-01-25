using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SystemSzwajcarski.Models
{
    public class User
    {
        public enum Role
        {
            Gracz,
            Organizator
        }
        [Key]
        public int idUser { get; set; }
        public string Login { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Role Roleuser { get; set; }

        public User()
        {

        }
        public User(UserRegister user,string password)
        {
            Name = user.Name;
            LastName = user.LastName;
            Login = user.Login;
            Email = user.Email;
            Password = password;
            if(user.Organizer)
            {
                Roleuser = Role.Organizator;
            }
            if (user.Player)
            {
                Roleuser = Role.Gracz;
            }
        }
        

    }
}
