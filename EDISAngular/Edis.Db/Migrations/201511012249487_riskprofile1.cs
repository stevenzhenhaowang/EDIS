namespace Edis.Db.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class riskprofile1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.RiskProfiles",
                c => new
                    {
                        RiskProfileID = c.String(nullable: false, maxLength: 128),
                        ClientID = c.String(nullable: false),
                        CapitalLossAttitude = c.String(),
                        Comments = c.String(),
                        DateCreated = c.String(),
                        DateModified = c.String(),
                        IncomeSource = c.String(),
                        InvestmentKnowledge = c.String(),
                        InvestmentObjective1 = c.String(),
                        InvestmentObjective2 = c.String(),
                        InvestmentObjective3 = c.String(),
                        InvestmentProfile = c.String(),
                        InvestmentTimeHorizon = c.String(),
                        LongTermGoal1 = c.String(),
                        LongTermGoal2 = c.String(),
                        LongTermGoal3 = c.String(),
                        MedTermGoal1 = c.String(),
                        MedTermGoal2 = c.String(),
                        MedTermGoal3 = c.String(),
                        RetirementAge = c.String(),
                        RetirementIncome = c.String(),
                        RiskAttitude = c.String(),
                        ShortTermAssetPercent = c.String(),
                        ShortTermEquityPercent = c.String(),
                        ShortTermGoal1 = c.String(),
                        ShortTermGoal2 = c.String(),
                        ShortTermGoal3 = c.String(),
                        ShortTermTrading = c.String(),
                        riskLevel = c.String(),
                    })
                .PrimaryKey(t => t.RiskProfileID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.RiskProfiles");
        }
    }
}
