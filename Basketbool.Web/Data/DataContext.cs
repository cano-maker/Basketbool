using Basketbool.Web.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Basketbool.Web.Data
{
    public class DataContext : IdentityDbContext<UserEntity>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<MatchDayDetailEntity> MatchDayDetails { get; set; }

        public DbSet<MatchDayEntity> MatchDays { get; set; }

        public DbSet<MatchEntity> Matches { get; set; }

        public DbSet<TeamEntity> Teams { get; set; }

        public DbSet<SeasonEntity> Seasons { get; set; }

        public DbSet<PredictionEntity> Predictions { get; set; }

        public DbSet<ConferenceEntity> Conferences { get; set; }

        public DbSet<DivisionEntity> Divisions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TeamEntity>()
                 .HasIndex(t => t.Name)
                 .IsUnique();

        }
    }
}
