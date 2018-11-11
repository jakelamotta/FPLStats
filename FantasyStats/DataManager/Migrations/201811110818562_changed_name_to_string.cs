namespace DataManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changed_name_to_string : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Position", "Name", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Position", "Name", c => c.Int(nullable: false));
        }
    }
}
