using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using SystemSzwajcarski.Models.Games;
using SystemSzwajcarski.Models.Relation;

namespace SystemSzwajcarski.Models.Main
{
    public class Game
    {
        [Key]
        public int idGame { get; set; }
        public int Round { get; set; }
        public bool Bye { get; set; }

        public Tournament Tournament { get; set; }
        public int? TournamentId { get; set; }
        public RelationTP BlackPlayer { get; set; }
        public int? BlackPlayerId { get; set; }

         public RelationTP WhitePlayer { get; set; }
        public int? WhitePlayerId { get; set; }

        public TypeResult Result { get; set; } = TypeResult.none;
        public Game()
        {

        }
        public Game(RelationTP player1, RelationTP player2,Tournament tournament)
        {
            BlackPlayer = player1;
            BlackPlayerId = player1.idRelation;
            WhitePlayer = player2;
            WhitePlayerId = player2.idRelation;
            Bye = false;
            Round = tournament.CurrentRound;
            Tournament = tournament;
            TournamentId = tournament.idTournament;
        }
        public Game(RelationTP player, Tournament tournament)
        {
            BlackPlayer = player;
            BlackPlayerId = player.idRelation;
            WhitePlayer = player;
            WhitePlayerId = player.idRelation;
            Bye = true;
            Round = tournament.CurrentRound;
            Tournament = tournament;
            TournamentId = tournament.idTournament;
        }
    }
}
