namespace Metropole.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class address1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Addresses", "FlatNumber", c => c.String());
            AddColumn("dbo.Addresses", "StreetAddress", c => c.String());
            DropColumn("dbo.Addresses", "ResidentialAddress");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Addresses", "ResidentialAddress", c => c.String());
            DropColumn("dbo.Addresses", "StreetAddress");
            DropColumn("dbo.Addresses", "FlatNumber");
        }
    }
}
