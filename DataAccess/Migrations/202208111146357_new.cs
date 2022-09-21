namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _new : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Approvals",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        RejectReason = c.String(),
                        ProjectID = c.Int(),
                        ManagerID = c.Int(),
                        statusID = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Logins", t => t.ManagerID)
                .ForeignKey("dbo.Projects", t => t.ProjectID)
                .ForeignKey("dbo.Status", t => t.statusID)
                .Index(t => t.ProjectID)
                .Index(t => t.ManagerID)
                .Index(t => t.statusID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Approvals", "statusID", "dbo.Status");
            DropForeignKey("dbo.Approvals", "ProjectID", "dbo.Projects");
            DropForeignKey("dbo.Approvals", "ManagerID", "dbo.Logins");
            DropIndex("dbo.Approvals", new[] { "statusID" });
            DropIndex("dbo.Approvals", new[] { "ManagerID" });
            DropIndex("dbo.Approvals", new[] { "ProjectID" });
            DropTable("dbo.Approvals");
        }
    }
}
