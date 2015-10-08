namespace Edis.Db.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Attachments",
                c => new
                    {
                        AttachmentId = c.String(nullable: false, maxLength: 128),
                        Title = c.String(nullable: false),
                        DateCreated = c.DateTime(nullable: false),
                        Path = c.String(),
                        DateModified = c.DateTime(nullable: false),
                        Comments = c.String(),
                        Data = c.Byte(nullable: false),
                        AttachmentType = c.String(),
                        NoteId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.AttachmentId)
                .ForeignKey("dbo.Notes", t => t.NoteId, cascadeDelete: true)
                .Index(t => t.NoteId);
            
            CreateTable(
                "dbo.Notes",
                c => new
                    {
                        NoteId = c.String(nullable: false, maxLength: 128),
                        DateCreated = c.DateTime(nullable: false),
                        Subject = c.String(nullable: false),
                        NoteType = c.Int(nullable: false),
                        SenderRole = c.Int(nullable: false),
                        DateModified = c.DateTime(nullable: false),
                        AdviserId = c.String(nullable: false),
                        AssetClass = c.String(),
                        ProductClass = c.String(),
                        Product = c.String(),
                        Purpose = c.String(),
                        TimeSpend = c.Single(nullable: false),
                        NoteSerial = c.String(),
                        Body = c.String(),
                        FollowupActions = c.String(),
                        DateDue = c.DateTime(nullable: false),
                        FollowupDate = c.DateTime(nullable: false),
                        DateCompleted = c.DateTime(nullable: false),
                        ReminderDate = c.DateTime(nullable: false),
                        Status = c.String(),
                        Reminder = c.Boolean(nullable: false),
                        IsAccepted = c.Boolean(nullable: false),
                        IsDeclined = c.Boolean(nullable: false),
                        AssetTypeId = c.String(),
                        AccountId = c.String(),
                        ClientId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.NoteId)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .Index(t => t.ClientId);
            
            CreateTable(
                "dbo.NoteLinks",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        DateCreated = c.DateTime(nullable: false),
                        NoteId1 = c.String(),
                        NoteId2 = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ResourcesReferences",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        TokenValue = c.String(),
                        FileExtension = c.String(),
                        ResourceUrl = c.String(),
                        UserId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Attachments", "NoteId", "dbo.Notes");
            DropForeignKey("dbo.Notes", "ClientId", "dbo.Clients");
            DropIndex("dbo.Notes", new[] { "ClientId" });
            DropIndex("dbo.Attachments", new[] { "NoteId" });
            DropTable("dbo.ResourcesReferences");
            DropTable("dbo.NoteLinks");
            DropTable("dbo.Notes");
            DropTable("dbo.Attachments");
        }
    }
}
