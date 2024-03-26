using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemSzwajcarski.Models.Main;

namespace SystemSzwajcarski.Models.Games
{
    public class RoundResult
    {
        public int idTournament { get; set; }
        public string Name { get; set; }
        public int CurrentRound { get; set; }
        public int MaxRound { get; set; }
        public string OrganizerName { get; set; }
        public string OrganizerLastName { get; set; }
        public List<GameResult> gameResults { get; set; } 
        public RoundResult(Tournament tournaments)
        {
            Name = tournaments.Name;
            CurrentRound = tournaments.CurrentRound;
            MaxRound = (int)tournaments.MaxRound;
            idTournament = tournaments.idTournament;
            OrganizerName = tournaments.Organizer.Name;
            OrganizerName = tournaments.Organizer.LastName;
            gameResults = new List<GameResult>();
        }
        public RoundResult()
        {

        }
    }
}
