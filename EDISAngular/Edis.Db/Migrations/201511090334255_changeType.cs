namespace Edis.Db.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeType : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CashAccounts", "Bsb", c => c.String());
            AlterColumn("dbo.CashAccounts", "AccountName", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CashAccounts", "AccountName", c => c.String(nullable: false));
            AlterColumn("dbo.CashAccounts", "Bsb", c => c.String(nullable: false));
        }
    }
}
