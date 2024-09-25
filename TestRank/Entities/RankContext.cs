using Microsoft.EntityFrameworkCore;

namespace TestRank.Entities
{
    public class RankContext : DbContext
    {
        public RankContext(DbContextOptions options) : base(options)
        {
        }

        public RankContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Score>()
                .HasKey(o => new { o.UserId, o.PlayDay });
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Score> Scores { get; set; }
    }
}
