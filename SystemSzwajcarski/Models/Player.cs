using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SystemSzwajcarski.Models
{
    public class Player:User
    {
        public List<RelationOP> Organizers { get; set; } = new List<RelationOP>();
        public Player()
        {
        }
        public Player(UserRegister user, string password) : base(user, password)
        {
        }
        public Player(PlayerAdd playeradd, string password)
        {
            LastName = playeradd.LastName;
            Login = playeradd.Login;
            Name = playeradd.Name;
            Password = password;
            Roleuser = Role.Gracz;
        }
    }
}
