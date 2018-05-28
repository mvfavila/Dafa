namespace DAFA.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20180325Entititesconfigurationfilesadded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EventType",
                c => new
                    {
                        EventTypeId = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 250, unicode: false),
                        NumberOfDaysToWarning = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.EventTypeId);
            
            CreateTable(
                "dbo.Event",
                c => new
                    {
                        EventId = c.Guid(nullable: false),
                        Name = c.String(maxLength: 100, unicode: false),
                        Description = c.String(maxLength: 100, unicode: false),
                        Date = c.DateTime(nullable: false),
                        EventTypeId = c.Guid(nullable: false),
                        ValidationResult_Message = c.String(maxLength: 100, unicode: false),
                        Field_FieldId = c.Guid(),
                    })
                .PrimaryKey(t => t.EventId)
                .ForeignKey("dbo.EventType", t => t.EventTypeId)
                .ForeignKey("dbo.Field", t => t.Field_FieldId)
                .Index(t => t.EventTypeId)
                .Index(t => t.Field_FieldId);
            
            CreateTable(
                "dbo.Registration",
                c => new
                    {
                        RegistrationId = c.Guid(nullable: false),
                        FieldId = c.Guid(nullable: false),
                        EventId = c.Guid(nullable: false),
                        PeriodicityId = c.Guid(nullable: false),
                        Active = c.Boolean(nullable: false),
                        ValidationResult_Message = c.String(maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.RegistrationId)
                .ForeignKey("dbo.Event", t => t.EventId)
                .ForeignKey("dbo.Field", t => t.FieldId)
                .ForeignKey("dbo.Periodicity", t => t.PeriodicityId)
                .Index(t => t.FieldId)
                .Index(t => t.EventId)
                .Index(t => t.PeriodicityId);
            
            CreateTable(
                "dbo.Field",
                c => new
                    {
                        FieldId = c.Guid(nullable: false),
                        Name = c.String(maxLength: 100, unicode: false),
                        Active = c.Boolean(nullable: false),
                        ClientId = c.Guid(nullable: false),
                        ValidationResult_Message = c.String(maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.FieldId)
                .ForeignKey("dbo.Client", t => t.ClientId)
                .Index(t => t.ClientId);
            
            CreateTable(
                "dbo.Client",
                c => new
                    {
                        ClientId = c.Guid(nullable: false),
                        Name = c.String(maxLength: 100, unicode: false),
                        Email = c.String(maxLength: 100, unicode: false),
                        Phone = c.String(maxLength: 100, unicode: false),
                        Active = c.Boolean(nullable: false),
                        ValidationResult_Message = c.String(maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.ClientId);
            
            CreateTable(
                "dbo.Periodicity",
                c => new
                    {
                        PeriodicityId = c.Guid(nullable: false),
                        Description = c.String(nullable: false, maxLength: 100, unicode: false),
                        Quantity = c.Int(nullable: false),
                        Unit = c.Int(nullable: false),
                        Active = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.PeriodicityId);
            
            CreateTable(
                "dbo.MenuItem",
                c => new
                    {
                        MenuItemId = c.Guid(nullable: false),
                        Name = c.String(maxLength: 100, unicode: false),
                        ActionName = c.String(maxLength: 100, unicode: false),
                        ControllerName = c.String(maxLength: 100, unicode: false),
                        Url = c.String(maxLength: 100, unicode: false),
                        ClaimType = c.String(maxLength: 100, unicode: false),
                        ClaimValue = c.String(maxLength: 100, unicode: false),
                    })
                .PrimaryKey(t => t.MenuItemId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Registration", "PeriodicityId", "dbo.Periodicity");
            DropForeignKey("dbo.Registration", "FieldId", "dbo.Field");
            DropForeignKey("dbo.Event", "Field_FieldId", "dbo.Field");
            DropForeignKey("dbo.Field", "ClientId", "dbo.Client");
            DropForeignKey("dbo.Registration", "EventId", "dbo.Event");
            DropForeignKey("dbo.Event", "EventTypeId", "dbo.EventType");
            DropIndex("dbo.Field", new[] { "ClientId" });
            DropIndex("dbo.Registration", new[] { "PeriodicityId" });
            DropIndex("dbo.Registration", new[] { "EventId" });
            DropIndex("dbo.Registration", new[] { "FieldId" });
            DropIndex("dbo.Event", new[] { "Field_FieldId" });
            DropIndex("dbo.Event", new[] { "EventTypeId" });
            DropTable("dbo.MenuItem");
            DropTable("dbo.Periodicity");
            DropTable("dbo.Client");
            DropTable("dbo.Field");
            DropTable("dbo.Registration");
            DropTable("dbo.Event");
            DropTable("dbo.EventType");
        }
    }
}
