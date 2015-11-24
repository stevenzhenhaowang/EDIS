namespace Edis.Db.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class whatchange : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Accounts", "MarginLenderId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Accounts", "MarginLenderId");
        }
    }
}
