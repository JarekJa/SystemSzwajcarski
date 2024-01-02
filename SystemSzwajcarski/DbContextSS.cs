using Microsoft.EntityFrameworkCore;
using SystemSzwajcarski.Models;

namespace SystemSzwajcarski
{
    public class DbContextSS: DbContext
    {
        public DbContextSS(DbContextOptions<DbContextSS> options) : base(options)
        {

        }
        public DbSet<Organizer> organizers { get; set; }
        public DbSet<Player> players { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
