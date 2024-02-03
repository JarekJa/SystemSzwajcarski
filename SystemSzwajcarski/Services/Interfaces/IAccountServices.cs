using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemSzwajcarski.Models;

namespace SystemSzwajcarski.Services.Interfaces
{
    public interface IAccountServices
    {
        public bool IsLogin(string login);
        bool Register(UserRegister user);
        string Login(UserLogin user);
        bool ConfirmUser(string token);
        string UserRole(string token);
        User GetUser(string token);
        bool Modifyuser(User user, UserRegister usernew);
        bool Modifypassord(User user, UserPasswords passowords);
        bool DelateUser(User user, UserLogin userLogin);
        public Player GetPlayer(string token);
        public Organizer GetOrganizer(string token);
    }
}
