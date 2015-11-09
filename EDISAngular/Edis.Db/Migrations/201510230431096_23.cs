namespace Edis.Db.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _23 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RebalanceModels", "ClientGroup_ClientGroupId", "dbo.ClientGroups");
            DropIndex("dbo.RebalanceModels", new[] { "ClientGroup_ClientGroupId" });
            AlterColumn("dbo.RebalanceModels", "ClientGroup_ClientGroupId", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.RebalanceModels", "ClientGroup_ClientGroupId");
            AddForeignKey("dbo.RebalanceModels", "ClientGroup_ClientGroupId", "dbo.ClientGroups", "ClientGroupId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RebalanceModels", "ClientGroup_ClientGroupId", "dbo.ClientGroups");
            DropIndex("dbo.RebalanceModels", new[] { "ClientGroup_ClientGroupId" });
            AlterColumn("dbo.RebalanceModels", "ClientGroup_ClientGroupId", c => c.String(maxLength: 128));
            CreateIndex("dbo.RebalanceModels", "ClientGroup_ClientGroupId");
            AddForeignKey("dbo.RebalanceModels", "ClientGroup_ClientGroupId", "dbo.ClientGroups", "ClientGroupId");
        }
    }
}
