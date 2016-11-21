namespace Team1_Final_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class samepage : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Songs", "SongAlbum_AlbumID", "dbo.Albums");
            DropForeignKey("dbo.AspNetUsers", "Song_SongID", "dbo.Songs");
            DropForeignKey("dbo.AspNetUsers", "Artist_ArtistID", "dbo.Artists");
            DropForeignKey("dbo.AspNetUsers", "Album_AlbumID", "dbo.Albums");
            DropIndex("dbo.AspNetUsers", new[] { "Song_SongID" });
            DropIndex("dbo.AspNetUsers", new[] { "Artist_ArtistID" });
            DropIndex("dbo.AspNetUsers", new[] { "Album_AlbumID" });
            DropIndex("dbo.Songs", new[] { "SongAlbum_AlbumID" });
            CreateTable(
                "dbo.SongAlbums",
                c => new
                    {
                        Song_SongID = c.Short(nullable: false),
                        Album_AlbumID = c.Short(nullable: false),
                    })
                .PrimaryKey(t => new { t.Song_SongID, t.Album_AlbumID })
                .ForeignKey("dbo.Songs", t => t.Song_SongID, cascadeDelete: true)
                .ForeignKey("dbo.Albums", t => t.Album_AlbumID, cascadeDelete: true)
                .Index(t => t.Song_SongID)
                .Index(t => t.Album_AlbumID);
            
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
            DropColumn("dbo.Songs", "SongAlbum_AlbumID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Songs", "SongAlbum_AlbumID", c => c.Short());
            AddColumn("dbo.AspNetUsers", "Album_AlbumID", c => c.Short());
            AddColumn("dbo.AspNetUsers", "Artist_ArtistID", c => c.Short());
            AddColumn("dbo.AspNetUsers", "Song_SongID", c => c.Short());
            DropForeignKey("dbo.AppUserSongs", "Song_SongID", "dbo.Songs");
            DropForeignKey("dbo.AppUserSongs", "AppUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AppUserAlbums", "Album_AlbumID", "dbo.Albums");
            DropForeignKey("dbo.AppUserAlbums", "AppUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.SongAlbums", "Album_AlbumID", "dbo.Albums");
            DropForeignKey("dbo.SongAlbums", "Song_SongID", "dbo.Songs");
            DropIndex("dbo.AppUserSongs", new[] { "Song_SongID" });
            DropIndex("dbo.AppUserSongs", new[] { "AppUser_Id" });
            DropIndex("dbo.AppUserAlbums", new[] { "Album_AlbumID" });
            DropIndex("dbo.AppUserAlbums", new[] { "AppUser_Id" });
            DropIndex("dbo.SongAlbums", new[] { "Album_AlbumID" });
            DropIndex("dbo.SongAlbums", new[] { "Song_SongID" });
            DropTable("dbo.AppUserSongs");
            DropTable("dbo.AppUserAlbums");
            DropTable("dbo.SongAlbums");
            CreateIndex("dbo.Songs", "SongAlbum_AlbumID");
            CreateIndex("dbo.AspNetUsers", "Album_AlbumID");
            CreateIndex("dbo.AspNetUsers", "Artist_ArtistID");
            CreateIndex("dbo.AspNetUsers", "Song_SongID");
            AddForeignKey("dbo.AspNetUsers", "Album_AlbumID", "dbo.Albums", "AlbumID");
            AddForeignKey("dbo.AspNetUsers", "Artist_ArtistID", "dbo.Artists", "ArtistID");
            AddForeignKey("dbo.AspNetUsers", "Song_SongID", "dbo.Songs", "SongID");
            AddForeignKey("dbo.Songs", "SongAlbum_AlbumID", "dbo.Albums", "AlbumID");
        }
    }
}
