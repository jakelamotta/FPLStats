namespace DataManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new_calc_value : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CalculatedPlayerStatistics", "xPTotal", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CalculatedPlayerStatistics", "xPTotal");
        }
    }
}
