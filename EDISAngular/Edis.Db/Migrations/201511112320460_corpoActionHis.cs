namespace Edis.Db.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class corpoActionHis : DbMigration
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
                        StockAdjustmentShareAmount = c.String(),
                        AssociatedAccountNumber = c.String(),
                        CorperateActionDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CorperateActionHistories");
        }
    }
}
