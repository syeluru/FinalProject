namespace Team1_Final_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class gottabesupercareful : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FeaturedItems",
                c => new
                    {
                        FeaturedItemID = c.Short(nullable: false, identity: true),
                        IsActiveFeaturedItem = c.Boolean(nullable: false),
                        FeaturedAlbum_AlbumID = c.Short(),
                        FeaturedArtist_ArtistID = c.Short(),
                        FeaturedSong_SongID = c.Short(),
                    })
                .PrimaryKey(t => t.FeaturedItemID)
                .ForeignKey("dbo.Albums", t => t.FeaturedAlbum_AlbumID)
                .ForeignKey("dbo.Artists", t => t.FeaturedArtist_ArtistID)
                .ForeignKey("dbo.Songs", t => t.FeaturedSong_SongID)
                .Index(t => t.FeaturedAlbum_AlbumID)
                .Index(t => t.FeaturedArtist_ArtistID)
                .Index(t => t.FeaturedSong_SongID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FeaturedItems", "FeaturedSong_SongID", "dbo.Songs");
            DropForeignKey("dbo.FeaturedItems", "FeaturedArtist_ArtistID", "dbo.Artists");
            DropForeignKey("dbo.FeaturedItems", "FeaturedAlbum_AlbumID", "dbo.Albums");
            DropIndex("dbo.FeaturedItems", new[] { "FeaturedSong_SongID" });
            DropIndex("dbo.FeaturedItems", new[] { "FeaturedArtist_ArtistID" });
            DropIndex("dbo.FeaturedItems", new[] { "FeaturedAlbum_AlbumID" });
            DropTable("dbo.FeaturedItems");
        }
    }
}
