namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class logs22233 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Audits", "Validation", c => c.String());
            AddColumn("dbo.Audits", "ResponseObject", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Audits", "ResponseObject");
            DropColumn("dbo.Audits", "Validation");
        }
    }
}
