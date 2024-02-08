using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemSzwajcarski.Models;
using SystemSzwajcarski.Models.Main;
using SystemSzwajcarski.Models.Tournaments;
using SystemSzwajcarski.Services.Interfaces;

namespace SystemSzwajcarski.Services
{
    public class TournamentsServices: ITournamentsServices
    {
        private readonly DbContextSS _dbContextSS;
        public TournamentsServices(DbContextSS dbContextSS)
        {
            _dbContextSS = dbContextSS;
        }
        public bool AddTournament(Organizer organizer, TournamentAdd tournamentAdd)
        {
            if(!(tournamentAdd.PrivateT ^ tournamentAdd.PublicT))
            {
                return false;
            }
            Tournament tournament = new Tournament(organizer,tournamentAdd);
            organizer.Tournament.Add(tournament);
            _dbContextSS.Tournaments.Add(tournament);
            bool successful =_dbContextSS.SaveChanges() > 0;
            tournamentAdd.id = tournament.idTournament;
            return successful;
        }
        public List<Tournament> GetMyTournaments(Organizer organizer)
        {
            _dbContextSS.Entry(organizer).Collection(sc => sc.Tournament).Load();
            return organizer.Tournament;
        }
    }
}
