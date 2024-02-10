using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemSzwajcarski.Models;
using SystemSzwajcarski.Models.Main;
using SystemSzwajcarski.Models.Tournaments;

namespace SystemSzwajcarski.Services.Interfaces
{
   public interface ITournamentsServices
    {
        public bool AddTournament(Organizer organizer, TournamentAdd tournamentAdd);
        public List<Tournament> GetMyTournaments(Organizer organizer);
        public PlayerstoAdd GetPLayertoAdd(Organizer organizer, int id);
        public bool AddPlayertoTournament(Organizer organizer, PlayerstoAdd playerstoAdd);
        public bool DeletePlayertoTournament(Organizer organizer, PlayerstoAdd playerstoAdd);
        public PlayerstoAdd GetPLayer(int id);
        public bool DeleteTournament(int id);
    }
}
