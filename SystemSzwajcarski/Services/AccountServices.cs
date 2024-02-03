using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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
        public bool IsLogin(string login)
        {
            User userpom;
            List<Organizer> organizers = _dbContextSS.organizers.ToList();
            userpom = organizers.Find(x => x.Login == login);
            if (userpom == null)
            {
                List<Player> players = _dbContextSS.players.ToList();
                userpom = players.Find(x => x.Login == login);
            }
            if (userpom != null)
            {
                return true;
            }
            return false;
        }
        private List<Claim> GetClaims(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokend = tokenHandler.ReadJwtToken(token);
            return tokend.Claims.ToList();
        }
        public bool Register(UserRegister user)
        {
            if (IsLogin(user.Login))
            {
                return false;
            }
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

            return _dbContextSS.SaveChanges() > 0;
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
        public bool DelateUser(User user,UserLogin userLogin)
        {
            if(user.Login==userLogin.Login && BCrypt.Net.BCrypt.Verify(userLogin.Password, user.Password))
            {
                if(user.Roleuser.ToString() == "Organizator")
                {
                    Organizer organizer = _dbContextSS.organizers.Find(user.idUser);
                    _dbContextSS.organizers.Remove(organizer);
                }
                else
                {
                    Player player = _dbContextSS.players.Find(user.idUser);
                    _dbContextSS.players.Remove(player);
                }
                return _dbContextSS.SaveChanges() > 0;
            }
            else
            {
                return false;
            }
        }
        public bool Modifypassord(User user,UserPasswords passowords)
        {
            if(user.Login == passowords.Login && BCrypt.Net.BCrypt.Verify(passowords.OldPassword, user.Password))
            {
                user.Password = BC.HashPassword(passowords.NewPassword);
                return _dbContextSS.SaveChanges() <= 0;
            }
            else
            {
                return false;
            }
        }
        public bool Modifyuser(User user, UserRegister usernew)
        {
            if (user.Login != usernew.Login)
            {
                if (IsLogin(usernew.Login))
                {
                    return false;
                }
            }
            user.Name = usernew.Name;
            user.LastName = usernew.LastName;
            user.Login = usernew.Login;
            user.Email = usernew.Email;
            return _dbContextSS.SaveChanges() > 0;
        }
        public string UserRole(string token)
        {
            List<Claim> clams = GetClaims(token);
            string role = clams[2].Value;
            return role;
        }
        public User GetUser(string token)
        {
            User user;
            List<Claim> clams = GetClaims(token);
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
        public Organizer GetOrganizer(string token)
        {
            Organizer user;
            List<Claim> clams = GetClaims(token);
            string login = clams[3].Value;
            string role = clams[2].Value;
            List<Organizer> organizers = _dbContextSS.organizers.Include(a => a.Players).ThenInclude(sc => sc.Player).ToList();
            user = organizers.Find(x => x.Login == login);
            return user;
        }
        public Player GetPlayer(string token)
        {
            Player user;
            List<Claim> clams = GetClaims(token);
            string login = clams[3].Value;
            string role = clams[2].Value;
            List<Player> players = _dbContextSS.players.ToList();
            user = players.Find(x => x.Login == login);
            return user;
        }
    }
}
