namespace Edis.Db.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addReinvestmentPlanDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ReinvestmentPlanActions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AdviserId = c.String(),
                        Ticker = c.String(),
                        ShareAmount = c.Double(nullable: false),
                        ReinvestmentDate = c.DateTime(nullable: false),
                        ParticipantsAccount = c.String(),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ReinvestmentPlanActions");
        }
    }
}
