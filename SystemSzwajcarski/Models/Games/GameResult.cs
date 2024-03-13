using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemSzwajcarski.Models.Main;

namespace SystemSzwajcarski.Models.Games
{
    public class GameResult
    {
        public int id { get; set; }
        public string WhiteName { get; set; }
        public string WhiteLastName { get; set; }

        public string BlackName { get; set; }
        public string BlackLastName { get; set; }
        public TypeResult Result { get; set; } = TypeResult.none;
        public GameResult(Game games)
        {
            WhiteName = games.WhitePlayer.Player.Name;
            WhiteLastName = games.WhitePlayer.Player.LastName;
            BlackName = games.BlackPlayer.Player.Name;
            BlackLastName = games.BlackPlayer.Player.LastName;
            id = games.idGame;
            Result = games.Result;
        }
        public GameResult()
        {

        }
    }
}
