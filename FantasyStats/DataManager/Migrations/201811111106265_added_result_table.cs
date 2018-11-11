namespace DataManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class added_result_table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CalculatedPlayerStatistics",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 128),
                        xPAbs = c.Double(nullable: false),
                        xPPound90 = c.Double(nullable: false),
                        xPPoundMinPlayed = c.Double(nullable: false),
                        MinutesPlayed = c.Double(nullable: false),
                        xYc = c.Double(nullable: false),
                        xRc = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Name);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CalculatedPlayerStatistics");
        }
    }
}
