namespace Edis.Db.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ResearchValues", "Value", c => c.Double());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ResearchValues", "Value", c => c.Double(nullable: false));
        }
    }
}
