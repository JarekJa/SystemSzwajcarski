using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SystemSzwajcarski.Models
{
    public class PlayerAdd
    {
        public int Id { get; set; }
        public string Login { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string LastName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Password { get; set; }
        [RegularExpression(@"\d{0,3}")]
        public string Ranking { get; set; }
        public PlayerAdd()
        {

        }
        public PlayerAdd(RelationOP player)
        {
            Id = player.idRelation;
            LastName = player.Player.LastName;
            Name = player.Player.Name;
            Email = player.Player.Email;
            Login = player.Player.Login;
            Ranking =player.Ranking.ToString();
        }
    }
}
