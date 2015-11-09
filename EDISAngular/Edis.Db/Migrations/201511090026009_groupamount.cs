namespace Edis.Db.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class groupamount : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ClientGroups", "GroupAmount", c => c.String());
            DropColumn("dbo.ClientGroups", "GroupAlias");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ClientGroups", "GroupAlias", c => c.String());
            DropColumn("dbo.ClientGroups", "GroupAmount");
        }
    }
}
