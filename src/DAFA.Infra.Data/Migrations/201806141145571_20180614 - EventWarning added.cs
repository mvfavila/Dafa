namespace DAFA.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20180614EventWarningadded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EventWarning",
                c => new
                    {
                        EventWarningId = c.Guid(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Solved = c.Boolean(nullable: false),
                        EventId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.EventWarningId)
                .ForeignKey("dbo.Event", t => t.EventId)
                .Index(t => t.EventId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EventWarning", "EventId", "dbo.Event");
            DropIndex("dbo.EventWarning", new[] { "EventId" });
            DropTable("dbo.EventWarning");
        }
    }
}
