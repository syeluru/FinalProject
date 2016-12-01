namespace Team1_Final_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class featureditemsallbuilt : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Discounts",
                c => new
                    {
                        DiscountID = c.Short(nullable: false, identity: true),
                        DiscountAmount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IsActiveDiscount = c.Boolean(nullable: false),
                        DiscountedAlbum_AlbumID = c.Short(),
                        DiscountedSong_SongID = c.Short(),
                    })
                .PrimaryKey(t => t.DiscountID)
                .ForeignKey("dbo.Albums", t => t.DiscountedAlbum_AlbumID)
                .ForeignKey("dbo.Songs", t => t.DiscountedSong_SongID)
                .Index(t => t.DiscountedAlbum_AlbumID)
                .Index(t => t.DiscountedSong_SongID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Discounts", "DiscountedSong_SongID", "dbo.Songs");
            DropForeignKey("dbo.Discounts", "DiscountedAlbum_AlbumID", "dbo.Albums");
            DropIndex("dbo.Discounts", new[] { "DiscountedSong_SongID" });
            DropIndex("dbo.Discounts", new[] { "DiscountedAlbum_AlbumID" });
            DropTable("dbo.Discounts");
        }
    }
}
