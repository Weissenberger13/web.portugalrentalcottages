namespace BootstrapVillas.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class whereAreWe : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PortugalSystems",
                c => new
                    {
                        PortugalSystemId = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        URL = c.String(),
                    })
                .PrimaryKey(t => t.PortugalSystemId);
            
            AddColumn("dbo.BookingExternal", "completed", c => c.Boolean());
            AddColumn("dbo.BookingExternal", "PortugalSystem_PortugalSystemId", c => c.Int());
            AddForeignKey("dbo.BookingExternal", "PortugalSystem_PortugalSystemId", "dbo.PortugalSystems", "PortugalSystemId");
            CreateIndex("dbo.BookingExternal", "PortugalSystem_PortugalSystemId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.BookingExternal", new[] { "PortugalSystem_PortugalSystemId" });
            DropForeignKey("dbo.BookingExternal", "PortugalSystem_PortugalSystemId", "dbo.PortugalSystems");
            DropColumn("dbo.BookingExternal", "PortugalSystem_PortugalSystemId");
            DropColumn("dbo.BookingExternal", "completed");
            DropTable("dbo.PortugalSystems");
        }
    }
}
