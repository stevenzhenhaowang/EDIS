namespace Edis.Db.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addedIndexForAssetPrice : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AssetPrices", "CorrespondingAssetKey", c => c.String(nullable: false, maxLength: 300));
            CreateIndex("dbo.AssetPrices", "CorrespondingAssetKey");
            CreateIndex("dbo.AssetPrices", "AssetType");
        }
        
        public override void Down()
        {
            DropIndex("dbo.AssetPrices", new[] { "AssetType" });
            DropIndex("dbo.AssetPrices", new[] { "CorrespondingAssetKey" });
            AlterColumn("dbo.AssetPrices", "CorrespondingAssetKey", c => c.String(nullable: false));
        }
    }
}
