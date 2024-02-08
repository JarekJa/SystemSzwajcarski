using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SystemSzwajcarski.Models.Tournaments
{
    public class TournamentAdd
    {
        public int id { get; set; }
        [Required]
        public string Name { get; set; }
        [RegularExpression(@"\d{0,4}")]
        public string MaxRound { get; set; }
        public bool PrivateT { get; set; } = false;
        public bool PublicT { get; set; } = false;
        public bool AddPlayer { get; set; } = false;
    }
}
