namespace DAFA.Infra.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _20180608FieldreferenceaddedtoEvent : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Event", new[] { "Field_FieldId" });
            RenameColumn(table: "dbo.Event", name: "Field_FieldId", newName: "FieldId");
            AlterColumn("dbo.Event", "FieldId", c => c.Guid(nullable: false));
            CreateIndex("dbo.Event", "FieldId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Event", new[] { "FieldId" });
            AlterColumn("dbo.Event", "FieldId", c => c.Guid());
            RenameColumn(table: "dbo.Event", name: "FieldId", newName: "Field_FieldId");
            CreateIndex("dbo.Event", "Field_FieldId");
        }
    }
}
