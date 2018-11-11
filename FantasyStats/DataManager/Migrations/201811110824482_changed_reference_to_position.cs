namespace DataManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changed_reference_to_position : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PlayerSeasonStatistics", "PositionId", c => c.Int(nullable: false));
            CreateIndex("dbo.PlayerSeasonStatistics", "PositionId");
            AddForeignKey("dbo.PlayerSeasonStatistics", "PositionId", "dbo.Position", "id");
            DropColumn("dbo.PlayerSeasonStatistics", "Position");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PlayerSeasonStatistics", "Position", c => c.Int(nullable: false));
            DropForeignKey("dbo.PlayerSeasonStatistics", "PositionId", "dbo.Position");
            DropIndex("dbo.PlayerSeasonStatistics", new[] { "PositionId" });
            DropColumn("dbo.PlayerSeasonStatistics", "PositionId");
        }
    }
}
