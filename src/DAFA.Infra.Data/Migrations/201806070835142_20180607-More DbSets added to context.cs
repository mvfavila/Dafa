namespace DAFA.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20180607MoreDbSetsaddedtocontext : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Event", "Name", c => c.String(nullable: false, maxLength: 200, unicode: false));
            AlterColumn("dbo.Event", "Description", c => c.String(nullable: false, maxLength: 250, unicode: false));
            AlterColumn("dbo.Field", "Name", c => c.String(nullable: false, maxLength: 250, unicode: false));
            AlterColumn("dbo.Client", "Name", c => c.String(nullable: false, maxLength: 200, unicode: false));
            AlterColumn("dbo.Client", "Email", c => c.String(nullable: false, maxLength: 100, unicode: false));
            AlterColumn("dbo.Client", "Phone", c => c.String(nullable: false, maxLength: 25, unicode: false));
            DropColumn("dbo.Event", "ValidationResult_Message");
            DropColumn("dbo.Field", "ValidationResult_Message");
            DropColumn("dbo.Client", "ValidationResult_Message");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Client", "ValidationResult_Message", c => c.String(maxLength: 100, unicode: false));
            AddColumn("dbo.Field", "ValidationResult_Message", c => c.String(maxLength: 100, unicode: false));
            AddColumn("dbo.Event", "ValidationResult_Message", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.Client", "Phone", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.Client", "Email", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.Client", "Name", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.Field", "Name", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.Event", "Description", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.Event", "Name", c => c.String(maxLength: 100, unicode: false));
        }
    }
}
