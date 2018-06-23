namespace DAFA.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20180623SolvedDateaddedtoEventWarningtable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EventWarning", "SolvedDate", c => c.DateTime());
            AddColumn("dbo.EventWarning", "ValidationResult_Message", c => c.String(maxLength: 100, unicode: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.EventWarning", "ValidationResult_Message");
            DropColumn("dbo.EventWarning", "SolvedDate");
        }
    }
}
