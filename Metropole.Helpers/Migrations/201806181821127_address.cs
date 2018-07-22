namespace Metropole.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class address : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ResidentialAddress = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AspNetUsers", "AddressId", c => c.Int(nullable: false));
            CreateIndex("dbo.AspNetUsers", "AddressId");
            AddForeignKey("dbo.AspNetUsers", "AddressId", "dbo.Addresses", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "AddressId", "dbo.Addresses");
            DropIndex("dbo.AspNetUsers", new[] { "AddressId" });
            DropColumn("dbo.AspNetUsers", "AddressId");
            DropTable("dbo.Addresses");
        }
    }
}
