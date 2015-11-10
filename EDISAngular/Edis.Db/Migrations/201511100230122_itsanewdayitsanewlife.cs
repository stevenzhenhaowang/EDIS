namespace Edis.Db.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class itsanewdayitsanewlife : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ReturnOfCapitals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AdviserId = c.String(),
                        CorperateActionName = c.String(),
                        ReturnCashAmount = c.Double(nullable: false),
                        ReturnDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.ClientGroups", "GroupAmount", c => c.String());
            AlterColumn("dbo.CashAccounts", "Bsb", c => c.String());
            AlterColumn("dbo.CashAccounts", "AccountName", c => c.String());
            DropColumn("dbo.ClientGroups", "GroupAlias");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ClientGroups", "GroupAlias", c => c.String());
            AlterColumn("dbo.CashAccounts", "AccountName", c => c.String(nullable: false));
            AlterColumn("dbo.CashAccounts", "Bsb", c => c.String(nullable: false));
            DropColumn("dbo.ClientGroups", "GroupAmount");
            DropTable("dbo.ReturnOfCapitals");
        }
    }
}
