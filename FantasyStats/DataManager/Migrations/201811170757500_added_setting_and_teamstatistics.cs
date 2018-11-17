namespace DataManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_setting_and_teamstatistics : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Setting",
                c => new
                    {
                        Key = c.String(nullable: false, maxLength: 128),
                        Value = c.String(),
                    })
                .PrimaryKey(t => t.Key);
            
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SeasonTeam", t => t.SeasonTeamId)
                .ForeignKey("dbo.Team", t => t.TeamId)
                .Index(t => t.SeasonTeamId)
                .Index(t => t.TeamId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TeamSeasonStatistics", "TeamId", "dbo.Team");
            DropForeignKey("dbo.TeamSeasonStatistics", "SeasonTeamId", "dbo.SeasonTeam");
            DropIndex("dbo.TeamSeasonStatistics", new[] { "TeamId" });
            DropIndex("dbo.TeamSeasonStatistics", new[] { "SeasonTeamId" });
            DropTable("dbo.TeamSeasonStatistics");
            DropTable("dbo.Setting");
        }
    }
}
