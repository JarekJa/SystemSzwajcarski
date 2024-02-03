using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemSzwajcarski.Models;
using SystemSzwajcarski.Services.Interfaces;
using BC = BCrypt.Net.BCrypt;

namespace SystemSzwajcarski.Services
{
    public class PlayersServices: IPlayersServices
    {
        private readonly DbContextSS _dbContextSS;
        public PlayersServices(DbContextSS dbContextSS)
        {
            _dbContextSS = dbContextSS;
        }
        public bool AddPlayer(Organizer organizer,PlayerAdd playeradd)
        {
            Player player;
            if(playeradd.Password!=null)
            {
                player = new Player(playeradd, BC.HashPassword(playeradd.Password));
            }
            else
            {
                player = new Player(playeradd,null);
            }
            RelationOP relationOP = new RelationOP(player,organizer,playeradd.Ranking);
            player.Organizers.Add(relationOP);
            organizer.Players.Add(relationOP);
            _dbContextSS.RelationOP.Add(relationOP);
            return _dbContextSS.SaveChanges() > 0;
        }

    }
}
