namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mig : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Logins",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        UserName = c.String(),
                        Password = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        Mobile = c.String(),
                        IsClient = c.Boolean(nullable: false),
                        IsDelete = c.Boolean(nullable: false),
                        DepartmentID = c.Int(),
                    })
                .PrimaryKey(t => t.UserID)
                .ForeignKey("dbo.Departments", t => t.DepartmentID)
                .Index(t => t.DepartmentID);
            
            CreateTable(
                "dbo.PermissionUsers",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        PermissionID = c.Int(),
                        UserID = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Logins", t => t.UserID)
                .ForeignKey("dbo.Permissions", t => t.PermissionID)
                .Index(t => t.PermissionID)
                .Index(t => t.UserID);
            
            CreateTable(
                "dbo.Permissions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ProjectClients",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        ProjectID = c.Int(),
                        ClientID = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Logins", t => t.ClientID)
                .ForeignKey("dbo.Projects", t => t.ProjectID)
                .Index(t => t.ProjectID)
                .Index(t => t.ClientID);
            
            CreateTable(
                "dbo.Projects",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ProjectEmps",
                c => new
                    {
                        ProjectID = c.Int(),
                        EmpID = c.Int(),
                        id = c.Int(nullable: false, identity: true),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Logins", t => t.EmpID)
                .ForeignKey("dbo.Projects", t => t.ProjectID)
                .Index(t => t.ProjectID)
                .Index(t => t.EmpID);
            
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        ProjectID = c.Int(nullable: false),
                        Description = c.String(),
                        statusID = c.Int(nullable: false),
                        ClientID = c.Int(),
                        AssignTo = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Logins", t => t.AssignTo)
                .ForeignKey("dbo.Logins", t => t.ClientID)
                .ForeignKey("dbo.Projects", t => t.ProjectID, cascadeDelete: true)
                .ForeignKey("dbo.Status", t => t.statusID, cascadeDelete: true)
                .Index(t => t.ProjectID)
                .Index(t => t.statusID)
                .Index(t => t.ClientID)
                .Index(t => t.AssignTo);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tickets", "statusID", "dbo.Status");
            DropForeignKey("dbo.Tickets", "ProjectID", "dbo.Projects");
            DropForeignKey("dbo.Tickets", "ClientID", "dbo.Logins");
            DropForeignKey("dbo.Tickets", "AssignTo", "dbo.Logins");
            DropForeignKey("dbo.ProjectClients", "ProjectID", "dbo.Projects");
            DropForeignKey("dbo.ProjectEmps", "ProjectID", "dbo.Projects");
            DropForeignKey("dbo.ProjectEmps", "EmpID", "dbo.Logins");
            DropForeignKey("dbo.ProjectClients", "ClientID", "dbo.Logins");
            DropForeignKey("dbo.PermissionUsers", "PermissionID", "dbo.Permissions");
            DropForeignKey("dbo.PermissionUsers", "UserID", "dbo.Logins");
            DropForeignKey("dbo.Logins", "DepartmentID", "dbo.Departments");
            DropIndex("dbo.Tickets", new[] { "AssignTo" });
            DropIndex("dbo.Tickets", new[] { "ClientID" });
            DropIndex("dbo.Tickets", new[] { "statusID" });
            DropIndex("dbo.Tickets", new[] { "ProjectID" });
            DropIndex("dbo.ProjectEmps", new[] { "EmpID" });
            DropIndex("dbo.ProjectEmps", new[] { "ProjectID" });
            DropIndex("dbo.ProjectClients", new[] { "ClientID" });
            DropIndex("dbo.ProjectClients", new[] { "ProjectID" });
            DropIndex("dbo.PermissionUsers", new[] { "UserID" });
            DropIndex("dbo.PermissionUsers", new[] { "PermissionID" });
            DropIndex("dbo.Logins", new[] { "DepartmentID" });
            DropTable("dbo.Tickets");
            DropTable("dbo.Status");
            DropTable("dbo.ProjectEmps");
            DropTable("dbo.Projects");
            DropTable("dbo.ProjectClients");
            DropTable("dbo.Permissions");
            DropTable("dbo.PermissionUsers");
            DropTable("dbo.Logins");
            DropTable("dbo.Departments");
        }
    }
}
