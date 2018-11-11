namespace DataManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplicationLog",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Username = c.String(maxLength: 255),
                        ApplicationUser = c.String(maxLength: 255),
                        Level = c.String(nullable: false, maxLength: 50),
                        Message = c.String(nullable: false),
                        Exception = c.String(),
                        ExtendedInformation = c.String(storeType: "ntext"),
                        SessionId = c.String(maxLength: 255),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.League",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Country = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PlayerCostChange",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PlayerId = c.Int(nullable: false),
                        Delta = c.Double(nullable: false),
                        ChangeCaptureDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Player", t => t.PlayerId)
                .Index(t => t.PlayerId);
            
            CreateTable(
                "dbo.Player",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                        SecondName = c.String(),
                        ExternalId = c.Int(nullable: false),
                        LastCost = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PlayerSeasonStatistics",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SeasonTeamId = c.Int(nullable: false),
                        PlayerId = c.Int(nullable: false),
                        BonusPointSystem = c.Int(nullable: false),
                        CleanSheets = c.Int(nullable: false),
                        OwnGoals = c.Int(nullable: false),
                        PenaltiesMissed = c.Int(nullable: false),
                        RedCards = c.Int(nullable: false),
                        YellowCards = c.Int(nullable: false),
                        Goals = c.Int(nullable: false),
                        Assists = c.Int(nullable: false),
                        MinutesPlayed = c.Int(nullable: false),
                        Apps = c.Int(nullable: false),
                        XG = c.Double(nullable: false),
                        XA = c.Double(nullable: false),
                        XG90 = c.Double(nullable: false),
                        XA90 = c.Double(nullable: false),
                        Position = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Player", t => t.PlayerId)
                .ForeignKey("dbo.SeasonTeam", t => t.SeasonTeamId)
                .Index(t => t.SeasonTeamId)
                .Index(t => t.PlayerId);
            
            CreateTable(
                "dbo.SeasonTeam",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TeamId = c.Int(nullable: false),
                        SeasonId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Season", t => t.SeasonId)
                .ForeignKey("dbo.Team", t => t.TeamId)
                .Index(t => t.TeamId)
                .Index(t => t.SeasonId);
            
            CreateTable(
                "dbo.Season",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StartYear = c.Int(nullable: false),
                        EndYear = c.Int(nullable: false),
                        LeagueId = c.Int(nullable: false),
                        IsCurrent = c.Boolean(nullable: false),
                        DataComplete = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.League", t => t.LeagueId)
                .Index(t => t.LeagueId);
            
            CreateTable(
                "dbo.Team",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PlayerSeasonStatistics", "SeasonTeamId", "dbo.SeasonTeam");
            DropForeignKey("dbo.SeasonTeam", "TeamId", "dbo.Team");
            DropForeignKey("dbo.SeasonTeam", "SeasonId", "dbo.Season");
            DropForeignKey("dbo.Season", "LeagueId", "dbo.League");
            DropForeignKey("dbo.PlayerSeasonStatistics", "PlayerId", "dbo.Player");
            DropForeignKey("dbo.PlayerCostChange", "PlayerId", "dbo.Player");
            DropIndex("dbo.Season", new[] { "LeagueId" });
            DropIndex("dbo.SeasonTeam", new[] { "SeasonId" });
            DropIndex("dbo.SeasonTeam", new[] { "TeamId" });
            DropIndex("dbo.PlayerSeasonStatistics", new[] { "PlayerId" });
            DropIndex("dbo.PlayerSeasonStatistics", new[] { "SeasonTeamId" });
            DropIndex("dbo.PlayerCostChange", new[] { "PlayerId" });
            DropTable("dbo.Team");
            DropTable("dbo.Season");
            DropTable("dbo.SeasonTeam");
            DropTable("dbo.PlayerSeasonStatistics");
            DropTable("dbo.Player");
            DropTable("dbo.PlayerCostChange");
            DropTable("dbo.League");
            DropTable("dbo.ApplicationLog");
        }
    }
}
