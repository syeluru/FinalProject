namespace Team1_Final_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hello : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MusicRatings",
                c => new
                    {
                        MusicRatingID = c.Int(nullable: false, identity: true),
                        RatingNumber = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Review = c.String(),
                        ReviewedAlbum_AlbumID = c.Short(),
                        ReviewedArtist_ArtistID = c.Short(),
                        ReviewedSong_SongID = c.Short(),
                    })
                .PrimaryKey(t => t.MusicRatingID)
                .ForeignKey("dbo.Albums", t => t.ReviewedAlbum_AlbumID)
                .ForeignKey("dbo.Artists", t => t.ReviewedArtist_ArtistID)
                .ForeignKey("dbo.Songs", t => t.ReviewedSong_SongID)
                .Index(t => t.ReviewedAlbum_AlbumID)
                .Index(t => t.ReviewedArtist_ArtistID)
                .Index(t => t.ReviewedSong_SongID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MusicRatings", "ReviewedSong_SongID", "dbo.Songs");
            DropForeignKey("dbo.MusicRatings", "ReviewedArtist_ArtistID", "dbo.Artists");
            DropForeignKey("dbo.MusicRatings", "ReviewedAlbum_AlbumID", "dbo.Albums");
            DropIndex("dbo.MusicRatings", new[] { "ReviewedSong_SongID" });
            DropIndex("dbo.MusicRatings", new[] { "ReviewedArtist_ArtistID" });
            DropIndex("dbo.MusicRatings", new[] { "ReviewedAlbum_AlbumID" });
            DropTable("dbo.MusicRatings");
        }
    }
}
