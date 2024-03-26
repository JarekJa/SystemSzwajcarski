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
        private readonly IGamesServices _gamesServices;
        public TournamentsServices(DbContextSS dbContextSS, IGamesServices gamesServices)
        {
            _dbContextSS = dbContextSS;
            _gamesServices = gamesServices;
        }
        public bool AddTournament(Organizer organizer, TournamentAdd tournamentAdd)
        {
            if(tournamentAdd.MaxRound!=null)
            {
                if(int.Parse(tournamentAdd.MaxRound)<=1)
                {
                    return false;
                }
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
        public List<Tournament> GetNoMyTournaments(Player player)
        {
            List<Tournament> tournaments = _dbContextSS.Tournaments.Where(sc=>sc.Status==TournamentStatus.tworzony).Where(sc => sc.Access != TournamentAccess.prywatny).Include(sc=>sc.Organizer).ToList();
           
            List<int> tournamentIds = _dbContextSS.Entry(player).Collection(sc => sc.Tournament).Query().Select(x => x.TournamentId).ToList() ;
            List<int> organizerIds = _dbContextSS.Entry(player).Collection(sc => sc.Organizers).Query().Select(x => x.OrganizerId).ToList();
            List<Tournament> noMyTournaments = new List<Tournament>();
            bool Added = false;
            bool HasOrganizer = false;
            foreach (Tournament tournament in tournaments)
            {
                Added = false;
                if (tournament.Access==TournamentAccess.orgraniczony)
                {
                    HasOrganizer = false;
                    foreach (int id in organizerIds)
                    {
                        if (tournament.Organizer.idUser==id)
                        {
                            HasOrganizer = true;
                        }
                    }
                    if(!HasOrganizer)
                    {
                        Added = true;
                    }
                }
                for (int i=0;i<tournamentIds.Count&&!Added;i++)
                {
                    if (tournament.idTournament == tournamentIds[i])
                    {
                        Added = true;
                    }
                }
                if (!Added)
                {
                   noMyTournaments.Add(tournament);
                }
            }
            return noMyTournaments;
        }
        public List<RelationTP> GetMyTournaments(Player player)
        {
            _dbContextSS.Entry(player).Collection(sc => sc.Tournament).Query().Include(sc=>sc.Tournament).ThenInclude(sc=>sc.Organizer).Load();
            return player.Tournament;
        }
        public PlayerstoAdd GetPLayertoAdd(Organizer organizer,int id)
        { 
            List<int> playerIds = _dbContextSS.Tournaments.Where(tournament => tournament.idTournament == id).SelectMany(tournament => tournament.Players.Select(player => player.PlayerId)).ToList();
            _dbContextSS.Entry(organizer).Collection(sc => sc.Players).Query().Include(sc => sc.Player).Load();
            List<RelationOP> relationOP = new List<RelationOP>();
            bool Added = false;
            foreach(RelationOP relation in organizer.Players)
            {
                Added = false;
                foreach(int i in playerIds)
                {
                    if(relation.PlayerId==i)
                    {
                        Added = true;
                    }
                }
                if(!Added)
                {
                    relationOP.Add(relation);
                }
            }
            PlayerstoAdd playerstoAdd = new PlayerstoAdd(relationOP, id);
            return playerstoAdd;
        }
        public PlayerstoAdd GetPLayer(int id)
        {
            Tournament tournament = _dbContextSS.Tournaments.Include(s => s.Players).ThenInclude(sc => sc.Player).FirstOrDefault(x => x.idTournament == id);
            PlayerstoAdd playerstoAdd = new PlayerstoAdd(tournament);
            return playerstoAdd;
        }
        public bool AddTournamentToPlayer(Player player, int id)
        {
            Tournament tournament = _dbContextSS.Tournaments.Include(sc=>sc.Organizer).FirstOrDefault(sc => sc.idTournament == id);
            RelationTP relationTP;
            if(tournament.Access==TournamentAccess.publiczny)
            {
                relationTP = new RelationTP(tournament,player);
                tournament.Players.Add(relationTP);
                tournament.NumberPlayers++;
            }
            else if(tournament.Access == TournamentAccess.orgraniczony)
            {
                RelationOP relationOP = _dbContextSS.Entry(tournament.Organizer).Collection(sc => sc.Players).Query().FirstOrDefault(sc=>sc.PlayerId==player.idUser);
                relationTP = new RelationTP(tournament,relationOP);
                tournament.Players.Add(relationTP);
                tournament.NumberPlayers++;
            }
            return _dbContextSS.SaveChanges() >= 0;
        }
        public bool AddPlayertoTournament(Organizer organizer, PlayerstoAdd playerstoAdd)
        {
            Tournament tournament = _dbContextSS.Tournaments.Where(x => x.idTournament ==playerstoAdd.TournamentId).FirstOrDefault();
            _dbContextSS.Entry(organizer).Collection(sc => sc.Players).Query().Include(sc => sc.Player).Load();
            for (int i = 0; i < playerstoAdd.idRelation.Count; i++)
            {
                if (playerstoAdd.ToAdd[i])
                {
                    RelationOP relationOP = organizer.Players.FirstOrDefault(x => x.idRelation == playerstoAdd.idRelation[i]);
                    RelationTP relationTP = new RelationTP(tournament, relationOP);
                    relationTP.Player.Tournament.Add(relationTP);
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
        public Tournament GetTournament(int id)
        {
            return _dbContextSS.Tournaments.Find(id);
        }
        public bool ModifyTournament(TournamentAdd tournamentAdd)
        {
            Tournament tournament = _dbContextSS.Tournaments.Find(tournamentAdd.id);
            tournament.Modify(tournamentAdd);
            return _dbContextSS.SaveChanges() >= 0;
        }
        public bool DeleteTournament(int id)
        {
            Tournament tournament= _dbContextSS.Tournaments.Include(sc=>sc.Games).FirstOrDefault(sc=>sc.idTournament==id);
            if (tournament.Status==TournamentStatus.tworzony)
            {
                _dbContextSS.Tournaments.Remove(tournament);
            }
            else if(tournament.Status == TournamentStatus.zakończony)
            {
                List<Game> game = _dbContextSS.games.Where(sc => sc.TournamentId == tournament.idTournament).ToList();
                _dbContextSS.games.RemoveRange(game);
                _dbContextSS.Tournaments.Remove(tournament);
            }
            return _dbContextSS.SaveChanges() > 0;
        }
        public bool StartTournament( int id)
        {
            Tournament tournament = _dbContextSS.Tournaments.Include(sc=>sc.Players).FirstOrDefault(sc=>sc.idTournament==id);
            int mround = (int) Math.Ceiling(Math.Log2(tournament.NumberPlayers));
            tournament.Status = TournamentStatus.trwający;
            if(tournament.MaxRound!= null)
            {
               if(tournament.MaxRound<1)
                {
                    tournament.MaxRound = mround;
                }
               if(tournament.MaxRound>tournament.NumberPlayers-1)
                {
                    tournament.MaxRound = tournament.NumberPlayers - 1;
                }           
            }
            else
            {
                tournament.MaxRound = mround;
            }
            if (!_gamesServices.FirstRound(tournament))
            {
                return false;
            }
            return _dbContextSS.SaveChanges() > 0;
        }

        public bool EndTournament(int id)
        {
            Tournament tournament = _dbContextSS.Tournaments.Include(sc=>sc.Games).FirstOrDefault(sc => sc.idTournament == id);
            if(tournament.CurrentRound<=1)
            {
                List<Game> game = _dbContextSS.games.Where(sc => sc.TournamentId==tournament.idTournament).ToList();
                tournament.Status = TournamentStatus.tworzony;
                tournament.CurrentRound =0;
                _dbContextSS.games.RemoveRange(game);
                _dbContextSS.SaveChanges();
                return false;
            }
            else
            {
                tournament.Status = TournamentStatus.zakończony;
            }
            return _dbContextSS.SaveChanges() >= 0;
        }
        public bool QuitTournament(int id)
        {
            RelationTP relationTP = _dbContextSS.RelationTP.Include(sc=>sc.Tournament).FirstOrDefault(sc=>sc.idRelation==id);
            relationTP.Tournament.NumberPlayers--;
            _dbContextSS.RelationTP.Remove(relationTP);
            return _dbContextSS.SaveChanges() > 0;
        }

    }
}
