namespace DataManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removed_fields_from_log : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ApplicationLog", "Username");
            DropColumn("dbo.ApplicationLog", "SessionId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ApplicationLog", "SessionId", c => c.String(maxLength: 255));
            AddColumn("dbo.ApplicationLog", "Username", c => c.String(maxLength: 255));
        }
    }
}
