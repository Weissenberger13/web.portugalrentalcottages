namespace BootstrapVillas.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class extraDocumentFieldsInBooking : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Booking", "FirewoodUnitPrice", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Booking", "FirewoodUnitPrice");
        }
    }
}
