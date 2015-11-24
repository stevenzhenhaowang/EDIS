namespace Edis.Db.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addMarginLender : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MarginLenders",
                c => new
                    {
                        LenderId = c.String(nullable: false, maxLength: 128),
                        LenderName = c.String(),
                    })
                .PrimaryKey(t => t.LenderId);
            
            AddColumn("dbo.LoanValueRatios", "MaxRatio", c => c.Double(nullable: false));
            AddColumn("dbo.LoanValueRatios", "EquityId", c => c.String(nullable: false));
            AddColumn("dbo.LoanValueRatios", "MarginLenderId", c => c.String());
            AlterColumn("dbo.LoanValueRatios", "ActiveDate", c => c.DateTime());
            DropColumn("dbo.LoanValueRatios", "Ratio");
            DropColumn("dbo.LoanValueRatios", "AssetId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LoanValueRatios", "AssetId", c => c.String(nullable: false));
            AddColumn("dbo.LoanValueRatios", "Ratio", c => c.Double(nullable: false));
            AlterColumn("dbo.LoanValueRatios", "ActiveDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.LoanValueRatios", "MarginLenderId");
            DropColumn("dbo.LoanValueRatios", "EquityId");
            DropColumn("dbo.LoanValueRatios", "MaxRatio");
            DropTable("dbo.MarginLenders");
        }
    }
}
