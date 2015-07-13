namespace BootstrapVillas.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class logoFile : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PRCInformation", "LogoFile", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PRCInformation", "LogoFile");
        }
    }
}
