namespace Edis.Db.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeMarginlending : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CorperateActionHistories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Status = c.Int(nullable: false),
                        ActionType = c.Int(nullable: false),
                        CorperateActionName = c.String(),
                        AdviserId = c.String(),
                        CashAdjustmentAmount = c.String(),
                        Ticker = c.String(),
                        StockAdjustmentShareAmount = c.String(),
                        AssociatedAccountId = c.String(),
                        ClientGroupId = c.String(),
                        CorperateActionDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ReinvestmentPlanActions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AdviserId = c.String(),
                        Ticker = c.String(),
                        ShareAmount = c.Double(nullable: false),
                        ReinvestmentDate = c.DateTime(nullable: false),
                        ParticipantsAccount = c.String(),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.MarginLendingTransactions", "Ratio", c => c.Double(nullable: false));
            AddColumn("dbo.MarginLendingTransactions", "AssetId", c => c.String(nullable: false));
            AddColumn("dbo.MarginLendingTransactions", "AssetTypes", c => c.Int(nullable: false));
            AddColumn("dbo.MarginLendingTransactions", "ActiveDate", c => c.DateTime());
            AlterColumn("dbo.MarginLendingTransactions", "GrantedOn", c => c.DateTime());
            AlterColumn("dbo.MarginLendingTransactions", "ExpiryDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MarginLendingTransactions", "ExpiryDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.MarginLendingTransactions", "GrantedOn", c => c.DateTime(nullable: false));
            DropColumn("dbo.MarginLendingTransactions", "ActiveDate");
            DropColumn("dbo.MarginLendingTransactions", "AssetTypes");
            DropColumn("dbo.MarginLendingTransactions", "AssetId");
            DropColumn("dbo.MarginLendingTransactions", "Ratio");
            DropTable("dbo.ReinvestmentPlanActions");
            DropTable("dbo.CorperateActionHistories");
        }
    }
}
