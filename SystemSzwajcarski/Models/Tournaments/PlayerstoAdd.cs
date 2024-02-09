using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemSzwajcarski.Models.Main;
using SystemSzwajcarski.Models.Relation;

namespace SystemSzwajcarski.Models.Tournaments
{
    public class PlayerstoAdd
    {
        public int TournamentId { get; set; }
        public List<int> idRelation { get; set; } = new List<int>();
        public List<string> Name { get; set; } = new List<string>();
        public List<string> LastName { get; set; } = new List<string>();
        public List<int> Ranking { get; set; } = new List<int>();

        public List<bool> ToAdd { get; set; } = new List<bool>();

        public PlayerstoAdd()
        {

        }
        public PlayerstoAdd(List<RelationOP> organizer,int id)
        {
            TournamentId = id;
            for(int i=0;i< organizer.Count;i++)
            {
                idRelation.Add(organizer[i].idRelation);
                Ranking.Add(organizer[i].Ranking);
                Name.Add(organizer[i].Player.Name);
                LastName.Add(organizer[i].Player.LastName);
                ToAdd.Add(false);
            }
        }
        public PlayerstoAdd(Tournament tournament)
        {
            TournamentId = tournament.idTournament;
            foreach(RelationTP player in tournament.Players)
            {
                idRelation.Add(player.idRelation);
                Ranking.Add(player.RankingPlayer);
                Name.Add(player.Player.Name);
                LastName.Add(player.Player.LastName);
                ToAdd.Add(true);
            }
        }
    }
}
