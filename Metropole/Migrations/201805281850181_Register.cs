namespace Metropole.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Register : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "NewsSubscription", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "WhatsAppMember", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "WhatsAppMember");
            DropColumn("dbo.AspNetUsers", "NewsSubscription");
        }
    }
}
