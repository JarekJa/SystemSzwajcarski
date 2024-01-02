using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SystemSzwajcarski.Models
{
    public class Player:User
    {
        public Player()
        {

        }
        public Player(User user, string password) : base(user, password)
        {

        }
    }
}
