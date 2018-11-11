namespace DataManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changed_name_to_string1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Position", "AppId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Position", "AppId");
        }
    }
}
