using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace DataManager.Models
{
    public class FPLStatsContext : DbContext
    {
        public FPLStatsContext()
            : base("DefaultConnection")
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<League> Leauges { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<SeasonTeam> SeasonTeams { get; set; }
        public DbSet<PlayerCostChange> PlayerCostChanges { get; set; }
        public DbSet<PlayerSeasonStatistics> PlayerSeasonStatistics { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<ApplicationLog> ApplicationLogs { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<CalculatedPlayerStatistics> CalculatedPlayerStatistics {get;set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }
}
