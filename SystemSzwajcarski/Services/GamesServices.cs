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
                relationTPs[tournament.NumberPlayers - 1].Bye = true;
                tournament.Games.Add(game);
            }

            return _dbContextSS.SaveChanges() > 0;
        }
        private bool CanPlay(RelationTP player1, RelationTP player2)
        {
            bool canPlay = true;
            if(player1.idRelation==player2.idRelation)
            {
                canPlay = false;
            }
            if(player1.Color+player2.Color>=4|| player1.Color + player2.Color <= -4)
            {
                canPlay = false;
            }
            else
            {
                foreach(Game game in player1.Games)
                {
                    if(game.BlackPlayerId==player2.idRelation|| game.WhitePlayerId == player2.idRelation)
                    {
                        canPlay = false;
                        break;
                    }
                }
            }
            return canPlay;
        }

        private void DutchSystem(Tournament tournament, List<RelationTP> relationTPs)
        {
            int j,i;
            int numberPlayer = tournament.NumberPlayers;
            bool playerfound;
            RelationTP relation1;
            RelationTP relation2;
            while (numberPlayer!=0)
            {
                i= 0;
                j = i + numberPlayer / 2;
                playerfound = false;
                while (!playerfound)
                {
                    if (CanPlay(relationTPs[i], relationTPs[j]))
                    {
                        Game game = new Game(relationTPs[j], relationTPs[i], tournament);
                        relation1 = relationTPs[j];
                        relation2 = relationTPs[i];
                        relationTPs[j].Games.Add(game);
                        relationTPs[j].Color--;
                        relationTPs[i].Games.Add(game);
                        relationTPs[i].Color++;
                        tournament.Games.Add(game);
                        relationTPs.Remove(relation1);
                        relationTPs.Remove(relation2);
                        numberPlayer -= 2;
                        playerfound = true;
                    }
                    if(j>=numberPlayer/2)
                    {
                        j++;
                    }
                    if(j==numberPlayer)
                    {
                        j = numberPlayer / 2 - 1;
                    }
                    if(j< numberPlayer / 2-1)
                    {
                        j--;
                    }
                    if(j==-1)
                    {
                        relation2 = relationTPs[i];
                        Game game = new Game(relationTPs[i], tournament);
                        relationTPs[i].Games.Add(game);
                        relationTPs[i].Bye = true;
                        tournament.Games.Add(game);
                        relationTPs.Remove(relation2);
                        numberPlayer--;
                        playerfound = true;
                    }
                }
            }
        }
        public bool GenerateRound(Tournament tournament)
        {
            tournament.CurrentRound++;
            List<RelationTP> relationTPs = tournament.Players.OrderByDescending(sc => sc.RankingTournament).ThenByDescending(sc => sc.RankingPlayer).ToList();
            DutchSystem(tournament, relationTPs);

            // return _dbContextSS.SaveChanges() > 0;
            return true;
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
        public bool ModifyResult(Tournament tournament,TournamentResult tournamentResult)
        { 
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

        public bool GameshaveResults(Tournament tournament)
        {
            bool haveResult = true;
            List<Game> games = tournament.Games.Where(sc=>sc.Round==tournament.CurrentRound).ToList();
            foreach(Game game in games)
            {
                if(!game.Bye)
                {
                   if(game.Result==TypeResult.none)
                    {
                        haveResult = false;
                    }
                }
            }
            return haveResult;
        }
        public bool ConfirmResult(Tournament tournament)
        {
            foreach(Game game in tournament.Games)
            {
                if(game.Bye)
                {
                    game.WhitePlayer.RankingTournament += 2;
                }
                else
                {
                    if(game.Result==TypeResult.white)
                    {
                        game.WhitePlayer.RankingTournament += 2;
                    }
                    else if(game.Result == TypeResult.black)
                    {
                        game.BlackPlayer.RankingTournament += 2;
                    }
                    else
                    {
                        game.WhitePlayer.RankingTournament += 1;
                        game.BlackPlayer.RankingTournament += 1;
                    }
                }
            }
            // return _dbContextSS.SaveChanges() >= 0;
            return true;
        }
        public Tournament GetTournament(int id)
        {
        Tournament tournament= _dbContextSS.Tournaments.Include(sc => sc.Games).ThenInclude(sc => sc.WhitePlayer).Include(sc => sc.Games).ThenInclude(sc => sc.BlackPlayer).Include(sc=>sc.Players).ThenInclude(sc=>sc.Games).FirstOrDefault(sc => sc.idTournament == id);
          
            return tournament;
        }
    }
}
