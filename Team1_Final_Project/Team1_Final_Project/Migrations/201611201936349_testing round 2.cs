namespace Team1_Final_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class testinground2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "Song_SongID", "dbo.Songs");
            DropForeignKey("dbo.AspNetUsers", "Artist_ArtistID", "dbo.Artists");
            DropForeignKey("dbo.AspNetUsers", "Album_AlbumID", "dbo.Albums");
            DropIndex("dbo.AspNetUsers", new[] { "Song_SongID" });
            DropIndex("dbo.AspNetUsers", new[] { "Artist_ArtistID" });
            DropIndex("dbo.AspNetUsers", new[] { "Album_AlbumID" });
            CreateTable(
                "dbo.AppUserAlbums",
                c => new
                    {
                        AppUser_Id = c.String(nullable: false, maxLength: 128),
                        Album_AlbumID = c.Short(nullable: false),
                    })
                .PrimaryKey(t => new { t.AppUser_Id, t.Album_AlbumID })
                .ForeignKey("dbo.AspNetUsers", t => t.AppUser_Id, cascadeDelete: true)
                .ForeignKey("dbo.Albums", t => t.Album_AlbumID, cascadeDelete: true)
                .Index(t => t.AppUser_Id)
                .Index(t => t.Album_AlbumID);
            
            CreateTable(
                "dbo.AppUserSongs",
                c => new
                    {
                        AppUser_Id = c.String(nullable: false, maxLength: 128),
                        Song_SongID = c.Short(nullable: false),
                    })
                .PrimaryKey(t => new { t.AppUser_Id, t.Song_SongID })
                .ForeignKey("dbo.AspNetUsers", t => t.AppUser_Id, cascadeDelete: true)
                .ForeignKey("dbo.Songs", t => t.Song_SongID, cascadeDelete: true)
                .Index(t => t.AppUser_Id)
                .Index(t => t.Song_SongID);
            
            DropColumn("dbo.AspNetUsers", "Song_SongID");
            DropColumn("dbo.AspNetUsers", "Artist_ArtistID");
            DropColumn("dbo.AspNetUsers", "Album_AlbumID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Album_AlbumID", c => c.Short());
            AddColumn("dbo.AspNetUsers", "Artist_ArtistID", c => c.Short());
            AddColumn("dbo.AspNetUsers", "Song_SongID", c => c.Short());
            DropForeignKey("dbo.AppUserSongs", "Song_SongID", "dbo.Songs");
            DropForeignKey("dbo.AppUserSongs", "AppUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AppUserAlbums", "Album_AlbumID", "dbo.Albums");
            DropForeignKey("dbo.AppUserAlbums", "AppUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.AppUserSongs", new[] { "Song_SongID" });
            DropIndex("dbo.AppUserSongs", new[] { "AppUser_Id" });
            DropIndex("dbo.AppUserAlbums", new[] { "Album_AlbumID" });
            DropIndex("dbo.AppUserAlbums", new[] { "AppUser_Id" });
            DropTable("dbo.AppUserSongs");
            DropTable("dbo.AppUserAlbums");
            CreateIndex("dbo.AspNetUsers", "Album_AlbumID");
            CreateIndex("dbo.AspNetUsers", "Artist_ArtistID");
            CreateIndex("dbo.AspNetUsers", "Song_SongID");
            AddForeignKey("dbo.AspNetUsers", "Album_AlbumID", "dbo.Albums", "AlbumID");
            AddForeignKey("dbo.AspNetUsers", "Artist_ArtistID", "dbo.Artists", "ArtistID");
            AddForeignKey("dbo.AspNetUsers", "Song_SongID", "dbo.Songs", "SongID");
        }
    }
}
