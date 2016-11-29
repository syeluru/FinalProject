namespace Team1_Final_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class allonsamepage1128552pm : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AlbumOrderBridges", "PriceAtPointOfPurchase", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.SongOrderBridges", "PriceAtPointOfPurchase", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.SongOrderBridges", "PriceAtPointOfPurchase");
            DropColumn("dbo.AlbumOrderBridges", "PriceAtPointOfPurchase");
        }
    }
}
