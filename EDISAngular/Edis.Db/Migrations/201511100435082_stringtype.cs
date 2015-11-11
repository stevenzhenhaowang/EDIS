namespace Edis.Db.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class stringtype : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ReturnOfCapitals", "ReturnCashAmount", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ReturnOfCapitals", "ReturnCashAmount", c => c.Double(nullable: false));
        }
    }
}
