using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemSzwajcarski.Models;
using SystemSzwajcarski.Services.Interfaces;

namespace SystemSzwajcarski.Controllers
{
    public class PlayersController : Controller
    {
        private readonly IAccountServices _accountS;
        private readonly IPlayersServices _playersS;
        public PlayersController(IAccountServices accountServices, IPlayersServices playersServices)
        {
            _accountS = accountServices;
            _playersS= playersServices; 
        }
        [HttpGet]
        public IActionResult AddPlayer()
        {
            string token = HttpContext.Session.GetString("Token");
            if (!_accountS.ConfirmUser(token))
            {
                return RedirectToAction("Index", "Home");
            }
            if (_accountS.UserRole(token) != "Organizator")
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost]
        public IActionResult AddPlayer(PlayerAdd playerAdd)
        {
            string token = HttpContext.Session.GetString("Token");
            if (!_accountS.ConfirmUser(token))
            {
                return RedirectToAction("Index", "Home");
            }
            if (_accountS.UserRole(token) != "Organizator")
            {
                return RedirectToAction("Index", "Home");
            }
            if (!ModelState.IsValid ||( _accountS.IsLogin(playerAdd.Login) && playerAdd.Login!=null))
            {
                return View(playerAdd);
            }
            Organizer organizer = _accountS.GetOrganizer(token);
            if(!_playersS.AddPlayer(organizer,playerAdd))
            {
                return View(playerAdd);

            }
            return RedirectToAction("GetMyPlayers", "Players");
        }
        public IActionResult GetMyPlayers()
        {
            string token = HttpContext.Session.GetString("Token");
            if (!_accountS.ConfirmUser(token))
            {
                return RedirectToAction("Index", "Home");
            }
            if (_accountS.UserRole(token) != "Organizator")
            {
                return RedirectToAction("Index", "Home");
            }
            Organizer organizer = _accountS.GetOrganizer(token);
            List<RelationOP> players = organizer.Players;
            return View(players);
        }
    }
}
