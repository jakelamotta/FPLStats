using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace DataManager.Models
{
    public class FPLStatsContext : DbContext
    {
        public FPLStatsContext()
            : base("FPLStatsConnection")
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public DbSet<League> Leauges { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<SeasonTeam> SeasonTeams { get; set; }
        public DbSet<SeasonTeamPlayer> SeasonTeamPlayers { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<ApplicationLog> ApplicationLogs { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
        }
    }
}
