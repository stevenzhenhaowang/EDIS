namespace Edis.Db.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteMaginlendingRefinLVR : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.LoanValueRatios", "MarginLendingTransaction_Id", "dbo.MarginLendingTransactions");
            DropIndex("dbo.LoanValueRatios", new[] { "MarginLendingTransaction_Id" });
            DropColumn("dbo.LoanValueRatios", "MarginLendingTransaction_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LoanValueRatios", "MarginLendingTransaction_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.LoanValueRatios", "MarginLendingTransaction_Id");
            AddForeignKey("dbo.LoanValueRatios", "MarginLendingTransaction_Id", "dbo.MarginLendingTransactions", "Id");
        }
    }
}
