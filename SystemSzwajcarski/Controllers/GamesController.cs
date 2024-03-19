using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemSzwajcarski.Models;
using SystemSzwajcarski.Models.Games;
using SystemSzwajcarski.Models.Main;
using SystemSzwajcarski.Services.Interfaces;

namespace SystemSzwajcarski.Controllers
{
    public class GamesController : Controller
    {
        private readonly IAccountServices _accountS;
        private readonly IGamesServices _gamesS;
        public GamesController(IAccountServices accountServices, IGamesServices gamesS)
        {
            _accountS = accountServices;
            _gamesS = gamesS;
        }
        public IActionResult ViewGamesPlayer(int id)
        {
            string token = HttpContext.Session.GetString("Token");
            if (!_accountS.ConfirmUser(token))
            {
                return RedirectToAction("Index", "Home");
            }
            if (_accountS.UserRole(token) != "Gracz")
            {
                return RedirectToAction("Index", "Home");
            }
            Player player = _accountS.GetPlayer(token);
            TournamentResult gameResults = _gamesS.GameResultsPlayer(player, id);
            return View(gameResults);
        }
        public IActionResult ViewGamesOrganizer(int id)
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
            TournamentResult gameResults = _gamesS.GameResultsOrganizer( id);
            return View(gameResults);
        }

        [HttpPost]
        public IActionResult ChangeResultOrganizer(TournamentResult tournamentResult, string Command)
        {
            string token = HttpContext.Session.GetString("Token");
            if (!_accountS.ConfirmUser(token))
            {
                return RedirectToAction("Index", "Home");
            }
            if (_accountS.UserRole(token) == "Organizator")
            {
                Tournament tournament = _gamesS.GetTournament(tournamentResult.idTournament); 
                if (!_gamesS.ModifyResult(tournament,tournamentResult))
                {
                    return View("ViewGamesOrganizer", tournamentResult);
                }
                if (Command == "Confirm")
                {
                    if(_gamesS.GameshaveResults(tournament))
                    {
                        if (_gamesS.ConfirmResult(tournament))
                        {
                            if (!_gamesS.GenerateRound(tournament))
                            {
                                return View("ViewGamesPlayer", tournamentResult);
                            }
                        }
                    }
                }
            }
            if (_accountS.UserRole(token) == "Gracz")
            {
                Tournament tournament = _gamesS.GetTournament(tournamentResult.idTournament);

                if (!_gamesS.ModifyResult(tournament,tournamentResult))
                {
                    return View("ViewGamesPlayer", tournamentResult);
                }
            }
            return RedirectToAction("GetMyTournaments", "Tournaments");
        }

    }
}
