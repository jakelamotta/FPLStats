namespace DataManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changed_double_to_int : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CalculatedPlayerStatistics", "MinutesPlayed", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CalculatedPlayerStatistics", "MinutesPlayed", c => c.Double(nullable: false));
        }
    }
}
