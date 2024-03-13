using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemSzwajcarski.Models;
using SystemSzwajcarski.Models.Main;
using SystemSzwajcarski.Models.Relation;
using SystemSzwajcarski.Models.Tournaments;

namespace SystemSzwajcarski.Services.Interfaces
{
   public interface ITournamentsServices
    {
        public bool AddTournament(Organizer organizer, TournamentAdd tournamentAdd);
        public List<Tournament> GetMyTournaments(Organizer organizer);
        public List<RelationTP> GetMyTournaments(Player player);
        public List<Tournament> GetNoMyTournaments(Player player);
        public PlayerstoAdd GetPLayertoAdd(Organizer organizer, int id);
        public bool AddPlayertoTournament(Organizer organizer, PlayerstoAdd playerstoAdd);
        public bool AddTournamentToPlayer(Player player, int id);
        public bool DeletePlayertoTournament(Organizer organizer, PlayerstoAdd playerstoAdd);
        public PlayerstoAdd GetPLayer(int id);
        public bool DeleteTournament(int id);
        public Tournament GetTournament(int id);
        public bool ModifyTournament(TournamentAdd tournamentAdd);
        public bool QuitTournament(int id);
        public bool StartTournament(int id);
    }
}
