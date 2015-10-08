namespace Edis.Db.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ResearchValues", "StringValue", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ResearchValues", "StringValue");
        }
    }
}
