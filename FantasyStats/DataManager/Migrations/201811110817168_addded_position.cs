namespace DataManager.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addded_position : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Position",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Name = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Position");
        }
    }
}
