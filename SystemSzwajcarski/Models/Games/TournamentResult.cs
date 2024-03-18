using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemSzwajcarski.Models.Main;

namespace SystemSzwajcarski.Models.Games
{
    public class TournamentResult
    {
        public int idTournament { get; set; }
        public string Name { get; set; }
        public int CurrentRound { get; set; }
        public int MaxRound { get; set; }
        public List<GameResult> gameResults { get; set; } 
        public TournamentResult(Tournament tournaments)
        {
            Name = tournaments.Name;
            CurrentRound = tournaments.CurrentRound;
            MaxRound = (int)tournaments.MaxRound;
            idTournament = tournaments.idTournament;
            gameResults = new List<GameResult>();
        }
        public TournamentResult()
        {

        }
    }
}
