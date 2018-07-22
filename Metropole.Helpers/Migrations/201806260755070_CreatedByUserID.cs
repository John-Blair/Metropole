namespace Metropole.Helpers.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreatedByUserID : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Responses", "CreatedByUserId", c => c.String());
            AddColumn("dbo.Surveys", "CreatedByUserId", c => c.String());
            AlterColumn("dbo.Surveys", "Name", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Surveys", "Name", c => c.String());
            DropColumn("dbo.Surveys", "CreatedByUserId");
            DropColumn("dbo.Responses", "CreatedByUserId");
        }
    }
}
