namespace EDIS.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifiedClientGroup : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ClientGroups", "AdviserNumber", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ClientGroups", "AdviserNumber", c => c.Int(nullable: false, identity: true));
        }
    }
}
