namespace Edis.Db.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class clientRebalance : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.RebalanceModels", "Adviser_AdviserId", "dbo.Advisers");
            DropIndex("dbo.RebalanceModels", new[] { "Adviser_AdviserId" });
            AddColumn("dbo.RebalanceModels", "Client_ClientId", c => c.String(maxLength: 128));
            AddColumn("dbo.ReturnOfCapitals", "AssociatedAccountNumber", c => c.String());
            AlterColumn("dbo.RebalanceModels", "Adviser_AdviserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.ReturnOfCapitals", "ReturnCashAmount", c => c.String());
            CreateIndex("dbo.RebalanceModels", "Adviser_AdviserId");
            CreateIndex("dbo.RebalanceModels", "Client_ClientId");
            AddForeignKey("dbo.RebalanceModels", "Client_ClientId", "dbo.Clients", "ClientId");
            AddForeignKey("dbo.RebalanceModels", "Adviser_AdviserId", "dbo.Advisers", "AdviserId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.RebalanceModels", "Adviser_AdviserId", "dbo.Advisers");
            DropForeignKey("dbo.RebalanceModels", "Client_ClientId", "dbo.Clients");
            DropIndex("dbo.RebalanceModels", new[] { "Client_ClientId" });
            DropIndex("dbo.RebalanceModels", new[] { "Adviser_AdviserId" });
            AlterColumn("dbo.ReturnOfCapitals", "ReturnCashAmount", c => c.Double(nullable: false));
            AlterColumn("dbo.RebalanceModels", "Adviser_AdviserId", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.ReturnOfCapitals", "AssociatedAccountNumber");
            DropColumn("dbo.RebalanceModels", "Client_ClientId");
            CreateIndex("dbo.RebalanceModels", "Adviser_AdviserId");
            AddForeignKey("dbo.RebalanceModels", "Adviser_AdviserId", "dbo.Advisers", "AdviserId", cascadeDelete: true);
        }
    }
}
