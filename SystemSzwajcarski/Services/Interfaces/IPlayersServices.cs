using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemSzwajcarski.Models;
using SystemSzwajcarski.Models.Players;

namespace SystemSzwajcarski.Services.Interfaces
{
   public interface IPlayersServices
    {
        public bool AddPlayer(Organizer organizer, PlayerAdd player);
        public bool ModifyRelationOP(RelationOP user, PlayerAdd usernew);
        public List<PlayerGet> GetPlayers(Organizer organizer);
        public List<RelationOP> GetMyPlayers(Organizer organizer);
        public bool Addtoorganizer(Organizer organizer, int id);
        public bool DelatePlayer(int id);
        public RelationOP GetRelationOP(Organizer organizer, int id);
    }
}
