using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SystemSzwajcarski.Models.Account;
using SystemSzwajcarski.Models.Main;
using SystemSzwajcarski.Models.Relation;

namespace SystemSzwajcarski.Models
{
    public class Player:User
    {
        public enum StatusAdd
        {
            Register,
            WithOrganizer,
            RegWithOrg,
            Deleted
        }
        public List<RelationOP> Organizers { get; set; } = new List<RelationOP>();
        public List<RelationTP> Tournament { get; set; } = new List<RelationTP>();
        public StatusAdd StatusCreatures { get; set; }
        public int Ranking { get; set; } = 0;
        public void ChangeStatus(int role)
        {
            switch(role)
            {
                case 1:
                    StatusCreatures = StatusAdd.Register;
                    break;
                case 2:
                    StatusCreatures = StatusAdd.WithOrganizer;
                    break;
                case 3:
                    StatusCreatures = StatusAdd.RegWithOrg;
                    break;
                case 4:
                    StatusCreatures = StatusAdd.Deleted;
                    break;
            }
        }
        public Player()
        {
        }
        public Player(UserRegister user, string password) : base(user, password)
        {
            StatusCreatures = StatusAdd.Register;
        }
        public Player(PlayerAdd playeradd, string password)
        {
            StatusCreatures = StatusAdd.WithOrganizer;
            LastName = playeradd.LastName;
            Login = playeradd.Login;
            Name = playeradd.Name;
            Password = password;
            Roleuser = Role.Gracz;
        }
    }
}
