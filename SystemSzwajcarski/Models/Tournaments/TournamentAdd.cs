using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using SystemSzwajcarski.Models.Main;

namespace SystemSzwajcarski.Models.Tournaments
{
    public class TournamentAdd
    {
        public int id { get; set; }
        [Required]
        public string Name { get; set; }
        [RegularExpression(@"\d{0,4}")]
        public string MaxRound { get; set; }
        public TournamentAccess Access { get; set; }

        public TournamentAdd()
        {

        }

        public TournamentAdd(Tournament tournament)
        {
            Name = tournament.Name;
            id = tournament.idTournament;
            Access = tournament.Access;   
            if (tournament.MaxRound!=null)
            {
                MaxRound = tournament.MaxRound.ToString();
            }
            else if (tournament.Access.ToString() == "publiczny")
            {
                MaxRound = "";
            }
        }
    }
}
