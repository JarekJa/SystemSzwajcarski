using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SystemSzwajcarski.Models
{
    public class Organizer:User
    {
        public List<RelationOP> Players { get; set; } = new List<RelationOP>();
        public Organizer()
        {
        }
        public Organizer(UserRegister user, string password) : base( user,  password)
        {
        }
    }
}
