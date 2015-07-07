namespace BootstrapVillas.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class notificationEmails : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PRCInformation", "PRCNotificationEmailAddress2", c => c.String());
            AddColumn("dbo.PRCInformation", "PRCNotificationEmailAddress3", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PRCInformation", "PRCNotificationEmailAddress3");
            DropColumn("dbo.PRCInformation", "PRCNotificationEmailAddress2");
        }
    }
}
