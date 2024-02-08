using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using SystemSzwajcarski.Models.Relation;
using SystemSzwajcarski.Models.Tournaments;

namespace SystemSzwajcarski.Models.Main
{
    public class Tournament
    {
        public enum TournamentAccess
        {
            prywatny ,
            publiczny
        }
        public enum TournamentStatus
        {
            tworzony,
            trwający,
            zakończony
        }
        [Key]
        public int idTournament { get; set; }
        public Organizer Organizer { get; set; }
        public List<Game> Games { get; set; } = new List<Game>();
        public TournamentStatus Status { get; set; }
        public TournamentAccess Access { get; set; }
        public List<RelationTP> Players { get; set; } = new List<RelationTP>();
        public int CurrentRound { get; set; } = 0;
        public int NumberPlayers { get; set; } = 0;
        public string Name { get; set; }
        public int? MaxRound { get; set; } = null;
        public Tournament()
        {
                
        }
        public Tournament(Organizer organizer, TournamentAdd tournamentAdd)
        {
            Organizer = organizer;
            Name = tournamentAdd.Name;
            Status = TournamentStatus.tworzony;
            if(tournamentAdd.MaxRound!=null)
            {
                MaxRound = int.Parse(tournamentAdd.MaxRound);
            }
            if (tournamentAdd.PrivateT)
            {
                Access = TournamentAccess.prywatny;
            }
            if (tournamentAdd.PublicT)
            {
                Access = TournamentAccess.publiczny;
            }
        }
    }
}
