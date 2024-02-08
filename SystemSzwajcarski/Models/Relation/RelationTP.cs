using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using SystemSzwajcarski.Models.Main;

namespace SystemSzwajcarski.Models.Relation
{
    public class RelationTP
    {
        [Key]
        public int idRelation { get; set; }
        public int RankingTournament { get; set; }
        public int RankingPlayer { get; set; }
        public Player Player { get; set; }
        public Tournament Tournament { get; set; }
        public int PlayerId { get; set; }
        public int TournamentId { get; set; }
        public List<Game> Games { get; set; } = new List<Game>();
        public int Black { get; set; }
    }
}
