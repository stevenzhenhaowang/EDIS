namespace Edis.Db.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addEquityTranIdToMarginLendingTran : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MarginLendingTransactions", "EquityTransactionId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.MarginLendingTransactions", "EquityTransactionId");
        }
    }
}
