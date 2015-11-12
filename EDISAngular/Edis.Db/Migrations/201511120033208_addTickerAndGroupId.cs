namespace Edis.Db.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTickerAndGroupId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CorperateActionHistories", "Ticker", c => c.String());
            AddColumn("dbo.CorperateActionHistories", "ClientGroupId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CorperateActionHistories", "ClientGroupId");
            DropColumn("dbo.CorperateActionHistories", "Ticker");
        }
    }
}
