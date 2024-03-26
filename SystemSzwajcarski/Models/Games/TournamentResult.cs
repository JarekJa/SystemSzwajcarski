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
        public int Round { get; set; }
        public string OrganizerName { get; set; }
        public string OrganizerLastName { get; set; }
        public List<PlayerResult> playerResults { get; set; }

        public TournamentResult()
        {

        }
        public TournamentResult(Tournament tournament)
        {
            Name = tournament.Name;
            Round = tournament.CurrentRound;
            idTournament = tournament.idTournament;
            OrganizerName = tournament.Organizer.Name;
            OrganizerName = tournament.Organizer.LastName;
            playerResults = new List<PlayerResult>();
        }
    }
}
