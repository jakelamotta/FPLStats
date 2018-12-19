namespace DataManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_stat : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PlayerSeasonStatistics", "Saves", c => c.Int(nullable: false));
            AddColumn("dbo.SeasonTeam", "CleanSheets", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SeasonTeam", "CleanSheets");
            DropColumn("dbo.PlayerSeasonStatistics", "Saves");
        }
    }
}
