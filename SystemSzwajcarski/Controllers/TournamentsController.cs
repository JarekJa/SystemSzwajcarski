using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemSzwajcarski.Models;
using SystemSzwajcarski.Models.Main;
using SystemSzwajcarski.Models.Tournaments;
using SystemSzwajcarski.Services.Interfaces;

namespace SystemSzwajcarski.Controllers
{
    public class TournamentsController : Controller
    {
        private readonly IAccountServices _accountS;
        private readonly ITournamentsServices _tournamentsS;
        public TournamentsController(IAccountServices accountServices, ITournamentsServices tournamentsS)
        {
            _accountS = accountServices;
            _tournamentsS = tournamentsS;
        }

        [HttpGet]
        public IActionResult AddTournament()
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
        public IActionResult AddTournament(TournamentAdd tournamentAdd, string Command)
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
                return View(tournamentAdd);
            }
            Organizer organizer = _accountS.GetOrganizer(token);
           if (!_tournamentsS.AddTournament(organizer, tournamentAdd))
            {
                return View(tournamentAdd);
            }
            if(Command== "Save")
            {
                return RedirectToAction("GetMyTournaments", "Tournaments");
            }
            else
            {
                return RedirectToAction("ViewPlayertoTournament", "Tournaments", new { Id = tournamentAdd.id });
            }
        }
        public IActionResult GetMyTournaments()
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
            List<Tournament> tournaments = _tournamentsS.GetMyTournaments(organizer);
            return View(tournaments);
        }
        public IActionResult ViewPlayertoTournament(int id)
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
            PlayerstoAdd playerstoAdd = _tournamentsS.GetPLayertoAdd(organizer,id); 
            return View(playerstoAdd);
        }
        public IActionResult ViewPlayer(int id)
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
            PlayerstoAdd playerstoAdd = _tournamentsS.GetPLayer(id);
            return View(playerstoAdd);
        }
        public IActionResult AddPlayertoTournament(PlayerstoAdd playerstoAdd)
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
            if (!_tournamentsS.AddPlayertoTournament(organizer, playerstoAdd))
            {
                return RedirectToAction("ViewPlayertoTournament", "Tournaments", new { Id = playerstoAdd.TournamentId });
            }
            return RedirectToAction("GetMyTournaments", "Tournaments");
        }
        public IActionResult DeletePlayertoTournament(PlayerstoAdd playerstoAdd)
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
            if (!_tournamentsS.DeletePlayertoTournament(organizer, playerstoAdd))
            {
                return RedirectToAction("ViewPlayertoTournament", "Tournaments", new { Id = playerstoAdd.TournamentId });
            }
            return RedirectToAction("GetMyTournaments", "Tournaments");
        }
        public IActionResult DeleteTournament(int id)
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
            _tournamentsS.DeleteTournament(id);
            return RedirectToAction("GetMyTournaments", "Tournaments");
        }
    }
}
