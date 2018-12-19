namespace DataManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_match_to_model : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Match",
                c => new
                    {
                        HomeTeamId = c.Int(nullable: false),
                        AwayTeamId = c.Int(nullable: false),
                        Id = c.Int(nullable: false, identity: true),
                        HomeGoals = c.Int(nullable: false),
                        HomeXGoals = c.Double(nullable: false),
                        HomeXGoalsAgainst = c.Double(nullable: false),
                        AwayGoals = c.Int(nullable: false),
                        AwayXGoals = c.Double(nullable: false),
                        AwayXGoalsAgainst = c.Double(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SeasonTeam", t => t.AwayTeamId)
                .ForeignKey("dbo.SeasonTeam", t => t.HomeTeamId)
                .Index(t => t.HomeTeamId)
                .Index(t => t.AwayTeamId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Match", "HomeTeamId", "dbo.SeasonTeam");
            DropForeignKey("dbo.Match", "AwayTeamId", "dbo.SeasonTeam");
            DropIndex("dbo.Match", new[] { "AwayTeamId" });
            DropIndex("dbo.Match", new[] { "HomeTeamId" });
            DropTable("dbo.Match");
        }
    }
}
