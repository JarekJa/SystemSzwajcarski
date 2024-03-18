using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemSzwajcarski.Models;
using SystemSzwajcarski.Models.Players;
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
            if (!ModelState.IsValid)
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
        public IActionResult Addtoorganizer(int id)
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
            _playersS.Addtoorganizer(organizer,id);
            return RedirectToAction("GetAllPlayers", "Players");
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
            List<RelationOP> players = _playersS.GetMyPlayers(organizer);
            return View(players);
        }
        public IActionResult GetAllPlayers()
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
            List<PlayerGet> players = _playersS.GetPlayers(organizer) ;
            return View(players);
        }

        public IActionResult Details(int id)
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
            RelationOP relationOP = _playersS.GetRelationOP(organizer,id);
            return View(relationOP);
        }
        public IActionResult Viewdata(int id)
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
            RelationOP relationOP = _playersS.GetRelationOP(organizer, id);
            return View(new PlayerAdd(relationOP));
        }
        [HttpPost]
        public IActionResult ChangeData(PlayerAdd usernew)
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
            if (!ModelState.IsValid)
            {
                return View("Viewdata", usernew);
            }
            Organizer organizer = _accountS.GetOrganizer(token);
            RelationOP relationOP = _playersS.GetRelationOP(organizer, usernew.Id);
            if (!_playersS.ModifyRelationOP(relationOP,usernew))
            {
                return View("Viewdata", usernew);
            }
            return RedirectToAction("Details", "Players", new { Id = usernew.Id });
        }
        public IActionResult DelatePlayer(int id)
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
            if (!_playersS.DelatePlayer(id))
            {
                return RedirectToAction("Details", "Players", new { Id = id });
            }
            return RedirectToAction("GetMyPlayers", "Players");
        }
    }
}
