namespace Edis.Db.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_cliengGroupId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.RebalanceModels", "ClientGroup_ClientGroupId", c => c.String(maxLength: 128));
            CreateIndex("dbo.RebalanceModels", "ClientGroup_ClientGroupId");
            AddForeignKey("dbo.RebalanceModels", "ClientGroup_ClientGroupId", "dbo.ClientGroups", "ClientGroupId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RebalanceModels", "ClientGroup_ClientGroupId", "dbo.ClientGroups");
            DropIndex("dbo.RebalanceModels", new[] { "ClientGroup_ClientGroupId" });
            DropColumn("dbo.RebalanceModels", "ClientGroup_ClientGroupId");
        }
    }
}
