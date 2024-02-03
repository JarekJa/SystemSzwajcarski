using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SystemSzwajcarski.Models
{
    public class RelationOP
    {
        [Key]
        public int idRelation { get; set; }
        public int Ranking { get; set; }
       public Player Player { get; set; }
       public Organizer Organizer  { get; set; }
        public int PlayerId { get; set; }
        public int OrganizerId { get; set; }
        public RelationOP()
        {

        }
        public RelationOP(Player Players, Organizer Organizers,string Ranking)
        {
            Player = Players;
            Organizer = Organizers;
            if (Ranking==null)
            {
                this.Ranking = 0;
            }
            else
            {
                this.Ranking = Int32.Parse(Ranking);
            }
        }

    }
}
