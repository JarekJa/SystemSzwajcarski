using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemSzwajcarski.Models;
using SystemSzwajcarski.Models.Players;
using SystemSzwajcarski.Services.Interfaces;
using BC = BCrypt.Net.BCrypt;

namespace SystemSzwajcarski.Services
{
    public class PlayersServices: IPlayersServices
    {
        private readonly IAccountServices _accountS;
        private readonly DbContextSS _dbContextSS;
        public PlayersServices(IAccountServices accountServices,DbContextSS dbContextSS)
        {
            _accountS = accountServices;
            _dbContextSS = dbContextSS;
        }
        public bool AddPlayer(Organizer organizer,PlayerAdd playeradd)
        {

            if(_accountS.IsLogin(playeradd.Login) && playeradd.Login != null)
            {
                return false;
            }
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
        public bool PlayerHasOrganize(Player player,Organizer organizer)
        {
            bool itis = false;
            foreach(RelationOP relationOP in organizer.Players)
            {
                if(player.idUser==relationOP.Player.idUser)
                {
                    itis = true;
                }
            }
            return itis;
        }
        public List<RelationOP> GetMyPlayers(Organizer organizer)
        {
            _dbContextSS.Entry(organizer).Collection(sc => sc.Players).Query().Include(sc => sc.Player).Load();
            return organizer.Players;
        }
        public RelationOP GetRelationOP(Organizer organizer, int id)
        {
            _dbContextSS.Entry(organizer).Collection(sc => sc.Players).Query().Where(x => x.idRelation == 1).Include(sc => sc.Player).Load();
            return organizer.Players.FirstOrDefault();
        }
        public List<PlayerGet> GetPlayers(Organizer organizer)
        {
            List<Player> players = _dbContextSS.players.ToList();
            List<PlayerGet> playerGets = new List<PlayerGet>();
            _dbContextSS.Entry(organizer).Collection(sc => sc.Players).Load();
            foreach (Player player in players)
            {
                PlayerGet playerGet = new PlayerGet();
                playerGet.Player = player;
                if(player.StatusCreatures.ToString()== "Register")
                {
                    playerGet.PlayerHasOrganizer = false;
                }
                else
                {
                    playerGet.PlayerHasOrganizer = PlayerHasOrganize(player, organizer);
                }
                playerGets.Add(playerGet);
            }
            return playerGets;
        }
        public bool Addtoorganizer(Organizer organizer, int id)
        {
            Player player = _dbContextSS.players.Find(id);
            player.ChangeStatus(3);
            RelationOP relationOP = new RelationOP(player,organizer,"0");
            organizer.Players.Add(relationOP);
            player.Organizers.Add(relationOP);
            _dbContextSS.RelationOP.Add(relationOP);
            return _dbContextSS.SaveChanges() > 0;
        }

            public bool ModifyRelationOP(RelationOP player, PlayerAdd playeradd)
          {
            _dbContextSS.Entry(player).Reference(sc => sc.Player).Load();
            if (playeradd.Login!=null)
            {
                if (player.Player.Login != playeradd.Login)
                {
                    if (_accountS.IsLogin(playeradd.Login))
                    {
                        return false;
                    }
                }
                
            }
            player.Player.Name = playeradd.Name;
            player.Player.LastName = playeradd.LastName;
            player.Player.Email = playeradd.Email;
            player.Player.Login = playeradd.Login;
            if (playeradd.Ranking != null)
            {
                player.Ranking = int.Parse(playeradd.Ranking);
            }
            if(playeradd.Password!=null)
            {
                player.Player.Password = BC.HashPassword(playeradd.Password);
            }
            return _dbContextSS.SaveChanges() >= 0;
        }

        public bool DelatePlayer(int id)
        {
            RelationOP relationOP = _dbContextSS.RelationOP.Find(id);
            Player player = relationOP.Player;
            if(player.StatusCreatures.ToString()== "Deleted"&&player.Organizers.Count==1)
            {
                _dbContextSS.players.Remove(player);
            }
            if (player.StatusCreatures.ToString() == "RegWithOrg" && player.Organizers.Count == 1)
            {
                player.ChangeStatus(1);
            }
            _dbContextSS.RelationOP.Remove(relationOP);
            return _dbContextSS.SaveChanges() >= 0;
        }

    }
}
