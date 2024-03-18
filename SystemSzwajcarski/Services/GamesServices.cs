using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemSzwajcarski.Models;
using SystemSzwajcarski.Models.Games;
using SystemSzwajcarski.Models.Main;
using SystemSzwajcarski.Models.Relation;
using SystemSzwajcarski.Services.Interfaces;

namespace SystemSzwajcarski.Services
{
    public class GamesServices: IGamesServices
    {
        private readonly DbContextSS _dbContextSS;
        public GamesServices(DbContextSS dbContextSS)
        {
            _dbContextSS = dbContextSS;
        }
        public bool FirstRound(Tournament tournament)
        {

            tournament.CurrentRound = 1;
            List<RelationTP> relationTPs = tournament.Players.OrderByDescending(sc=>sc.RankingPlayer).ToList();
            for(int i=0;i<tournament.NumberPlayers/2;i++)
            {
                Game game = new Game(relationTPs[i+ tournament.NumberPlayers / 2], relationTPs[i],tournament);
                relationTPs[i + tournament.NumberPlayers / 2].Games.Add(game);
                relationTPs[i + tournament.NumberPlayers / 2].Color--;
                relationTPs[i].Games.Add(game);
                relationTPs[i].Color++;
                tournament.Games.Add(game);
            }
            if(tournament.NumberPlayers%2==1)
            {
                Game game = new Game(relationTPs[tournament.NumberPlayers-1],tournament);
                relationTPs[tournament.NumberPlayers - 1].Games.Add(game);
                tournament.Games.Add(game);
            }

            return _dbContextSS.SaveChanges() > 0;
        }
        public TournamentResult GameResultsOrganizer(int id)
        {
            Tournament tournament = _dbContextSS.Tournaments.FirstOrDefault(sc => sc.idTournament == id);
            TournamentResult gameResults = new TournamentResult(tournament); 
            List<Game> games =_dbContextSS.Entry(tournament).Collection(sc => sc.Games).Query().Where(sc => sc.Round == tournament.CurrentRound).Include(sc => sc.WhitePlayer).ThenInclude(sc => sc.Player).Include(sc => sc.BlackPlayer).ThenInclude(sc => sc.Player).ToList();
           foreach(Game game in games)
            {
                gameResults.gameResults.Add(new GameResult(game));
            }
            return gameResults;
        }
        public TournamentResult GameResultsPlayer(Player player,int id)
        {
            Tournament tournament = _dbContextSS.Tournaments.FirstOrDefault(sc => sc.idTournament == id);
            TournamentResult gameResults = new TournamentResult(tournament);
            RelationTP relatioTP = _dbContextSS.Entry(player).Collection(sc => sc.Tournament).Query().FirstOrDefault(sc => sc.TournamentId == id);
            List<Game> games = _dbContextSS.Entry(tournament).Collection(sc => sc.Games).Query().Where(sc => sc.Round == tournament.CurrentRound).Include(sc => sc.WhitePlayer).ThenInclude(sc => sc.Player).Include(sc => sc.BlackPlayer).ThenInclude(sc => sc.Player).ToList();
            foreach (Game game in games)
            {
                if (game.BlackPlayerId==relatioTP.idRelation||game.BlackPlayerId== relatioTP.idRelation)
                {
                    gameResults.gameResults.Add(new GameResult(game));
                }
            }
            return gameResults;
        }
        public bool ModifyResult(TournamentResult tournamentResult)
        {
            Tournament tournament = _dbContextSS.Tournaments.Include(s => s.Games).FirstOrDefault(sc=>sc.idTournament==tournamentResult.idTournament);
            foreach(GameResult gameResult in tournamentResult.gameResults)
            {
                for (int i=0;i<tournament.Games.Count;i++)
                {
                    if(gameResult.id== tournament.Games[i].idGame)
                    {
                        tournament.Games[i].Result = gameResult.Result;
                        i = tournament.Games.Count;
                    }
                }
            }
            return _dbContextSS.SaveChanges() >= 0;
        }

    }
}
