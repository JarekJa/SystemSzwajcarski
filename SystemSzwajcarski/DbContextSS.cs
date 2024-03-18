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
            builder.Entity<Game>()
               .HasKey(g => g.idGame);

            builder.Entity<Game>()
    .HasOne(g => g.Tournament)
    .WithMany()
    .HasForeignKey(g => g.TournamentId);
            builder.Entity<Game>()
                .HasOne(g => g.WhitePlayer)
                .WithMany()
                .HasForeignKey(g => g.WhitePlayerId);

            builder.Entity<Game>()
                .HasOne(g => g.BlackPlayer)
                .WithMany()
                .HasForeignKey(g => g.BlackPlayerId);
            base.OnModelCreating(builder);
        }
    }
}
