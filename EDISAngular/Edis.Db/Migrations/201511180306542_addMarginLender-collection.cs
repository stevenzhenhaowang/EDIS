namespace Edis.Db.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addMarginLendercollection : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LoanValueRatios", "MarginLender_LenderId", c => c.String(maxLength: 128));
            CreateIndex("dbo.LoanValueRatios", "MarginLender_LenderId");
            AddForeignKey("dbo.LoanValueRatios", "MarginLender_LenderId", "dbo.MarginLenders", "LenderId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LoanValueRatios", "MarginLender_LenderId", "dbo.MarginLenders");
            DropIndex("dbo.LoanValueRatios", new[] { "MarginLender_LenderId" });
            DropColumn("dbo.LoanValueRatios", "MarginLender_LenderId");
        }
    }
}
