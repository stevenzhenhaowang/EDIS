namespace Edis.Db.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class makeitBackToNumber : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ReturnOfCapitals", "AssociatedAccountNumber", c => c.String());
            DropColumn("dbo.ReturnOfCapitals", "AssociatedAccountId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ReturnOfCapitals", "AssociatedAccountId", c => c.String());
            DropColumn("dbo.ReturnOfCapitals", "AssociatedAccountNumber");
        }
    }
}
