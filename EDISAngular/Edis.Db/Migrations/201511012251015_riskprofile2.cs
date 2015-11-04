namespace Edis.Db.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class riskprofile2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RiskProfiles", "riskLevel", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RiskProfiles", "riskLevel", c => c.String());
        }
    }
}
