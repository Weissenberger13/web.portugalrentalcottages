namespace BootstrapVillas.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Emailtemplate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EmailTemplates",
                c => new
                    {
                        EmailTemplateId = c.Int(nullable: false, identity: true),
                        EmailTemplateName = c.String(),
                        EmailTemplateBodyHTML = c.String(),
                    })
                .PrimaryKey(t => t.EmailTemplateId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.EmailTemplates");
        }
    }
}
