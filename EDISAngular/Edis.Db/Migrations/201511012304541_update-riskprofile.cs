namespace Edis.Db.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateriskprofile : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.RiskProfiles", "DateCreated", c => c.DateTime());
            AlterColumn("dbo.RiskProfiles", "DateModified", c => c.DateTime());
            AlterColumn("dbo.RiskProfiles", "InvestmentTimeHorizon", c => c.Int());
            AlterColumn("dbo.RiskProfiles", "RetirementAge", c => c.Int());
            AlterColumn("dbo.RiskProfiles", "RetirementIncome", c => c.Double());
            AlterColumn("dbo.RiskProfiles", "ShortTermAssetPercent", c => c.Double());
            AlterColumn("dbo.RiskProfiles", "ShortTermEquityPercent", c => c.Double());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.RiskProfiles", "ShortTermEquityPercent", c => c.String());
            AlterColumn("dbo.RiskProfiles", "ShortTermAssetPercent", c => c.String());
            AlterColumn("dbo.RiskProfiles", "RetirementIncome", c => c.String());
            AlterColumn("dbo.RiskProfiles", "RetirementAge", c => c.String());
            AlterColumn("dbo.RiskProfiles", "InvestmentTimeHorizon", c => c.String());
            AlterColumn("dbo.RiskProfiles", "DateModified", c => c.String());
            AlterColumn("dbo.RiskProfiles", "DateCreated", c => c.String());
        }
    }
}
