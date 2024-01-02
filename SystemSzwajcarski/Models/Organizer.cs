using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SystemSzwajcarski.Models
{
    public class Organizer:User
    {
        public Organizer()
        {

        }
        public Organizer(User user, string password) : base( user,  password)
        {
                
        }
    }
}
