namespace BootstrapVillas.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class depositExcessInstance : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.BookingExtraSelection", "DepositExcessEUROInstance", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.BookingExtraSelection", "DepositExcessEUROInstance");
        }
    }
}
