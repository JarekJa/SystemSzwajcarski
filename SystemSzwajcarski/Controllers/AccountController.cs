using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemSzwajcarski.Models;
using SystemSzwajcarski.Services.Interfaces;

namespace SystemSzwajcarski.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountServices _accountS;
        public AccountController(IAccountServices accountServices)
        {
            _accountS = accountServices;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(User user)
        {
            if (!ModelState.IsValid || !(user.Player ^ user.Organizer))
            {
                return View(user);
            }

            if(_accountS.Register(user))
            {
                return View(user);
            }
            return RedirectToAction("Index", "Home"); ;
        }
    }
}
