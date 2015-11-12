namespace Edis.Db.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class countrycode2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CountryCodes",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Code = c.String(nullable: false),
                        CountryName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CountryCodes");
        }
    }
}
