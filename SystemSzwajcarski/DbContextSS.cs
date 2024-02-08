using Microsoft.EntityFrameworkCore;
using SystemSzwajcarski.Models;
using SystemSzwajcarski.Models.Main;
using SystemSzwajcarski.Models.Relation;

namespace SystemSzwajcarski
{
    public class DbContextSS: DbContext
    {
        public DbContextSS(DbContextOptions<DbContextSS> options) : base(options)
        {

        }
        public DbSet<Organizer> organizers { get; set; }
        public DbSet<Player> players { get; set; }
        public DbSet<RelationOP> RelationOP { get; set; }
        public DbSet<Game> games { get; set; }
        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<RelationTP> RelationTP { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
