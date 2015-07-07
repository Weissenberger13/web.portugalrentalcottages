namespace BootstrapVillas.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class currencyLongName : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Booking", "BookingCurrencyLongName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Booking", "BookingCurrencyLongName");
        }
    }
}
