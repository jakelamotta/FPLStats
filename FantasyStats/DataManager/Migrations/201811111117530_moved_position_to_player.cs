namespace DataManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class moved_position_to_player : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PlayerSeasonStatistics", "PositionId", "dbo.Position");
            DropIndex("dbo.PlayerSeasonStatistics", new[] { "PositionId" });
            AddColumn("dbo.Player", "PositionId", c => c.Int(nullable: false));
            CreateIndex("dbo.Player", "PositionId");
            AddForeignKey("dbo.Player", "PositionId", "dbo.Position", "id");
            DropColumn("dbo.PlayerSeasonStatistics", "PositionId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PlayerSeasonStatistics", "PositionId", c => c.Int(nullable: false));
            DropForeignKey("dbo.Player", "PositionId", "dbo.Position");
            DropIndex("dbo.Player", new[] { "PositionId" });
            DropColumn("dbo.Player", "PositionId");
            CreateIndex("dbo.PlayerSeasonStatistics", "PositionId");
            AddForeignKey("dbo.PlayerSeasonStatistics", "PositionId", "dbo.Position", "id");
        }
    }
}
