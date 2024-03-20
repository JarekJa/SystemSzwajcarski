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
        public int RankingTournament { get; set; } = 0;
        public int RankingPlayer { get; set; } = 0;
        public Player Player { get; set; }
        public Tournament Tournament { get; set; }
        public int PlayerId { get; set; }
        public int TournamentId { get; set; }
        public List<Game> Games { get; set; } = new List<Game>();
        public int Color { get; set; } = 0;
        // black = false;
        public bool? ColorLastGame { get; set; } = null;
        public bool Bye { get; set; } = false;
        public RelationTP()
        {
                
        }

        public RelationTP(Tournament tournament,RelationOP relationOP)
        {
            Tournament = tournament;
            TournamentId = tournament.idTournament;
            Player = relationOP.Player;
            PlayerId = relationOP.PlayerId;
            if(tournament.Access.ToString()== "prywatny")
            {
                RankingPlayer = relationOP.Ranking;
            }
            else
            {
                RankingPlayer = relationOP.Player.Ranking;
            }
        }
        public RelationTP(Tournament tournament,Player player)
        {
            Tournament = tournament;
            TournamentId = tournament.idTournament;
            Player = player;
            PlayerId = player.idUser;
            RankingPlayer = player.Ranking;
        }
    }


}
