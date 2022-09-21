namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _2033 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Projects", "statusID", c => c.Int());
            CreateIndex("dbo.Projects", "statusID");
            AddForeignKey("dbo.Projects", "statusID", "dbo.Status", "id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Projects", "statusID", "dbo.Status");
            DropIndex("dbo.Projects", new[] { "statusID" });
            DropColumn("dbo.Projects", "statusID");
        }
    }
}
