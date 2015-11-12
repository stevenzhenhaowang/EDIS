namespace Edis.Db.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class numberToAccountId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CorperateActionHistories", "AssociatedAccountId", c => c.String());
            DropColumn("dbo.CorperateActionHistories", "AssociatedAccountNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CorperateActionHistories", "AssociatedAccountNumber", c => c.String());
            DropColumn("dbo.CorperateActionHistories", "AssociatedAccountId");
        }
    }
}
