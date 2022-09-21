namespace DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migLog : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Audits", "Bug", c => c.String());
            AddColumn("dbo.Audits", "Response", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Audits", "Response");
            DropColumn("dbo.Audits", "Bug");
        }
    }
}
