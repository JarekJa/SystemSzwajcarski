using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemSzwajcarski.Models;
using SystemSzwajcarski.Services.Interfaces;
using BC=BCrypt.Net.BCrypt;


namespace SystemSzwajcarski.Services
{
   
    public class AccountServices : IAccountServices
    {
        private readonly DbContextSS _dbContextSS;
        public AccountServices(DbContextSS dbContextSS)
        {
            _dbContextSS = dbContextSS;
        }
        public bool Register(User user)
        {
            if (user.Organizer)
            { 
                 Organizer newuser = new Organizer(user, BC.HashPassword(user.Password));
                _dbContextSS.organizers.Add(newuser);
            }
            if (user.Player)
            {
                Player newuser = new Player(user, BC.HashPassword(user.Password));
                _dbContextSS.players.Add(newuser);
            }

            
            return _dbContextSS.SaveChanges()<=0;
        }

    }
}
