namespace DataManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changed_seasonteams : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.TeamSeasonStatistics", "SeasonTeamId", "dbo.SeasonTeam");
            DropForeignKey("dbo.TeamSeasonStatistics", "TeamId", "dbo.Team");
            DropIndex("dbo.TeamSeasonStatistics", new[] { "SeasonTeamId" });
            DropIndex("dbo.TeamSeasonStatistics", new[] { "TeamId" });
            AddColumn("dbo.SeasonTeam", "Position", c => c.Int(nullable: false));
            AddColumn("dbo.SeasonTeam", "GamesPlayed", c => c.Int(nullable: false));
            AddColumn("dbo.SeasonTeam", "Won", c => c.Int(nullable: false));
            AddColumn("dbo.SeasonTeam", "Drawn", c => c.Int(nullable: false));
            AddColumn("dbo.SeasonTeam", "Lost", c => c.Int(nullable: false));
            AddColumn("dbo.SeasonTeam", "Points", c => c.Int(nullable: false));
            AddColumn("dbo.SeasonTeam", "XGFor", c => c.Double(nullable: false));
            AddColumn("dbo.SeasonTeam", "XGAgainst", c => c.Double(nullable: false));
            AddColumn("dbo.SeasonTeam", "XPoints", c => c.Double(nullable: false));
            DropTable("dbo.TeamSeasonStatistics");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.TeamSeasonStatistics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SeasonTeamId = c.Int(nullable: false),
                        TeamId = c.Int(nullable: false),
                        GamesPlayed = c.Int(nullable: false),
                        Won = c.Int(nullable: false),
                        Drawn = c.Int(nullable: false),
                        Lost = c.Int(nullable: false),
                        Points = c.Int(nullable: false),
                        xG = c.Double(nullable: false),
                        xGA = c.Double(nullable: false),
                        xP = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.SeasonTeam", "XPoints");
            DropColumn("dbo.SeasonTeam", "XGAgainst");
            DropColumn("dbo.SeasonTeam", "XGFor");
            DropColumn("dbo.SeasonTeam", "Points");
            DropColumn("dbo.SeasonTeam", "Lost");
            DropColumn("dbo.SeasonTeam", "Drawn");
            DropColumn("dbo.SeasonTeam", "Won");
            DropColumn("dbo.SeasonTeam", "GamesPlayed");
            DropColumn("dbo.SeasonTeam", "Position");
            CreateIndex("dbo.TeamSeasonStatistics", "TeamId");
            CreateIndex("dbo.TeamSeasonStatistics", "SeasonTeamId");
            AddForeignKey("dbo.TeamSeasonStatistics", "TeamId", "dbo.Team", "Id");
            AddForeignKey("dbo.TeamSeasonStatistics", "SeasonTeamId", "dbo.SeasonTeam", "Id");
        }
    }
}
