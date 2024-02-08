using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using SystemSzwajcarski.Models.Relation;

namespace SystemSzwajcarski.Models.Main
{
    public class Game
    {
        public enum TypeResult
        {
            win,
            lost,
            draw
        }
        [Key]
        public int idGame { get; set; }
        public int Round { get; set; }

        public RelationTP Relation { get; set; }
        public int RelationId { get; set; }
        public Player Opponent { get; set; }
        public int? OpponentId { get; set; }

        public TypeResult Result { get; set; }
        // black = false
        public bool? Color { get; set; } = null;
    }
}
