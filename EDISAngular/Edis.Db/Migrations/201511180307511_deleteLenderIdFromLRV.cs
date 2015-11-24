namespace Edis.Db.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteLenderIdFromLRV : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.LoanValueRatios", "MarginLenderId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.LoanValueRatios", "MarginLenderId", c => c.String());
        }
    }
}
