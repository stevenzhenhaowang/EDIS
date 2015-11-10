namespace Edis.Db.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _new : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ReturnOfCapitals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AdviserId = c.String(),
                        CorperateActionName = c.String(),
                        ReturnCashAmount = c.Double(nullable: false),
                        ReturnDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ReturnOfCapitals");
        }
    }
}
