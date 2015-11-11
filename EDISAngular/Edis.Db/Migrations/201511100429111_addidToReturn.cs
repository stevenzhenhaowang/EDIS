namespace Edis.Db.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addidToReturn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ReturnOfCapitals", "AssociatedAccountId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ReturnOfCapitals", "AssociatedAccountId");
        }
    }
}
