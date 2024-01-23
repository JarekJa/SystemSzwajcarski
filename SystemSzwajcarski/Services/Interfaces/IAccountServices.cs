using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemSzwajcarski.Models;

namespace SystemSzwajcarski.Services.Interfaces
{
    public interface IAccountServices
    {
        bool Register(UserRegister user);
        string Login(UserLogin user);
    }
}
