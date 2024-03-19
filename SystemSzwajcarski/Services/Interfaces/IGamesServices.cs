using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemSzwajcarski.Models;
using SystemSzwajcarski.Models.Games;
using SystemSzwajcarski.Models.Main;

namespace SystemSzwajcarski.Services.Interfaces
{
    public interface IGamesServices
    {
        public bool FirstRound(Tournament tournament);
        public TournamentResult GameResultsOrganizer(int i);
        public TournamentResult GameResultsPlayer(Player player,int i);
        public bool ModifyResult(Tournament tournament, TournamentResult tournamentResult);
        public Tournament GetTournament(int id);
        public bool GameshaveResults(Tournament tournament);
        public bool GenerateRound(Tournament tournament);
        public bool ConfirmResult(Tournament tournament);
    }
}
