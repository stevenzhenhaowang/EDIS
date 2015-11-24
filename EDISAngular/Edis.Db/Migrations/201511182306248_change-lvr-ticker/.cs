namespace Edis.Db.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changelvrticker : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.LoanValueRatios", "Ticker", c => c.String(nullable: false));
            DropColumn("dbo.LoanValueRatios", "EquityId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LoanValueRatios", "EquityId", c => c.String(nullable: false));
            DropColumn("dbo.LoanValueRatios", "Ticker");
        }
    }
}
