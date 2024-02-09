using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemSzwajcarski.Models;
using SystemSzwajcarski.Models.Main;
using SystemSzwajcarski.Models.Relation;
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
        public PlayerstoAdd GetPLayertoAdd(Organizer organizer,int id)
        {
            _dbContextSS.Entry(organizer).Collection(sc => sc.Players).Query().Include(sc => sc.Player).Load();
            PlayerstoAdd playerstoAdd = new PlayerstoAdd(organizer.Players,id);
            return playerstoAdd;
        }
        public PlayerstoAdd GetPLayer(int id)
        {
            Tournament tournament = _dbContextSS.Tournaments.Include(s => s.Players).ThenInclude(sc => sc.Player).FirstOrDefault(x => x.idTournament == id);
            PlayerstoAdd playerstoAdd = new PlayerstoAdd(tournament);
            return playerstoAdd;
        }
        public bool AddPlayertoTournament(Organizer organizer, PlayerstoAdd playerstoAdd)
        {
            Tournament tournament = _dbContextSS.Tournaments.Where(x => x.idTournament ==playerstoAdd.TournamentId).FirstOrDefault();
            _dbContextSS.Entry(organizer).Collection(sc => sc.Players).Query().Include(sc => sc.Player).Load();
            for (int i = 0; i < playerstoAdd.idRelation.Count; i++)
            {
                if (playerstoAdd.ToAdd[i])
                {
                    RelationTP relationTP = new RelationTP(tournament, organizer.Players.FirstOrDefault(x => x.idRelation == playerstoAdd.idRelation[i]));
                    organizer.Players[i].Player.Tournament.Add(relationTP);
                    tournament.Players.Add(relationTP);
                    tournament.NumberPlayers++;
                }
            }       
            return _dbContextSS.SaveChanges() >= 0;
        }
        public bool DeletePlayertoTournament(Organizer organizer, PlayerstoAdd playerstoAdd)
        {
            Tournament tournament = _dbContextSS.Tournaments.Where(x => x.idTournament == playerstoAdd.TournamentId).FirstOrDefault();
            List<RelationTP> relationTPs = _dbContextSS.RelationTP.ToList();
            List<RelationTP> ToRemove = new List<RelationTP>();
            for (int i = 0; i < playerstoAdd.idRelation.Count; i++)
            {
                if (!playerstoAdd.ToAdd[i])
                {
                    RelationTP relationTP = relationTPs.FirstOrDefault(x=>x.idRelation==playerstoAdd.idRelation[i]);
                    ToRemove.Add(relationTP);
                    tournament.NumberPlayers--;
                }
            }
            _dbContextSS.RelationTP.RemoveRange(ToRemove);
            return _dbContextSS.SaveChanges() >= 0;
        }

    }
}
