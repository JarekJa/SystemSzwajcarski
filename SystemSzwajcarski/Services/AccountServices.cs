using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
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
        public string Login(UserLogin userlog)
        {
            List<Organizer> organizers = _dbContextSS.organizers.ToList();
           User user = organizers.Find(x => x.Login == userlog.Login);
            if (user == null)
            {
                List<Player> players = _dbContextSS.players.ToList();
                user = players.Find(x => x.Login == userlog.Login);
            }
            if (user != null && BCrypt.Net.BCrypt.Verify(userlog.Password, user.Password))
            {
                generatedToken = _tokenService.BuildToken(_config["Jwt:Key"].ToString(), _config["Jwt:Issuer"].ToString(), user);
                if (generatedToken != null)
                {
                    return generatedToken;
                }
            }
            return "";
        }

        public bool Register(UserRegister user)
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
        public bool ConfirmUser(string token)
        {
            if (token == null)
            {
                return false;
            }
            if (!_tokenService.ValidateToken(_config["Jwt:Key"].ToString(), _config["Jwt:Issuer"].ToString(), token))
            {
                return false;
            }
            return true;
        }

        public string UserRole(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokend = tokenHandler.ReadJwtToken(token);
            List<Claim> clams = tokend.Claims.ToList();
            string role = clams[2].Value;
            return role;
        }

        public User GetUser(string token)
        {
            User user;
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokend = tokenHandler.ReadJwtToken(token);
            List<Claim> clams = tokend.Claims.ToList();
            string login = clams[3].Value;
            string role = clams[2].Value;
            if(role=="Organizator")
            {
                List<Organizer> organizers = _dbContextSS.organizers.ToList();
                user = organizers.Find(x => x.Login == login);
            }
            else
            {
                List<Player> players = _dbContextSS.players.ToList();
                user = players.Find(x => x.Login == login);
            }
            return user;
        }
    }
}
