using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemSzwajcarski.Models.Relation;

namespace SystemSzwajcarski.Models.Games
{
    public class PlayerResult
    {
        public int idRelation { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public int Result { get; set; }
        public PlayerResult()
        {

    }
        public PlayerResult(RelationTP relationTP)
        {
            idRelation = relationTP.idRelation;
            Name = relationTP.Player.Name;
            LastName = relationTP.Player.LastName;
            Result = relationTP.RankingTournament;
        }
    }
}
