namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig88 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Departments", "ManagerID", c => c.Int());
            AlterColumn("dbo.Tickets", "Description", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tickets", "Description", c => c.String());
            DropColumn("dbo.Departments", "ManagerID");
        }
    }
}
