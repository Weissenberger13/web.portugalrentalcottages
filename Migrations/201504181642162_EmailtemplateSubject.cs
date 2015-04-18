namespace BootstrapVillas.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmailtemplateSubject : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EmailTemplates", "EmailSubject", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.EmailTemplates", "EmailSubject");
        }
    }
}
