using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
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
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _config; 
        private string generatedToken = null;
        public AccountServices(IConfiguration config, DbContextSS dbContextSS, ITokenService tokenService)
        {
            _dbContextSS = dbContextSS;
            _tokenService = tokenService;
            _config = config;
        }
        public string Login(UserLogin user)
        {
            List<Organizer> organizers = _dbContextSS.organizers.ToList();
            User user1 = organizers.Find(x => x.Login == user.Login);
            if (user1 == null)
            {
                List<Player> players = _dbContextSS.players.ToList();
                user1 = players.Find(x => x.Login == user.Login);
            }
            if (user1 != null && BCrypt.Net.BCrypt.Verify(user.Password, user1.Password))
            {
                generatedToken = _tokenService.BuildToken(_config["Jwt:Key"].ToString(), _config["Jwt:Issuer"].ToString(), user1);
                if (generatedToken != null)
                {
                    return generatedToken;
                }
            }
            return "";
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
