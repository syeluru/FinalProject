namespace Team1_Final_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixingstuff : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.SongAlbums", "Song_SongID", "dbo.Songs");
            DropForeignKey("dbo.SongAlbums", "Album_AlbumID", "dbo.Albums");
            DropForeignKey("dbo.AppUserAlbums", "AppUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AppUserAlbums", "Album_AlbumID", "dbo.Albums");
            DropForeignKey("dbo.AppUserSongs", "AppUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AppUserSongs", "Song_SongID", "dbo.Songs");
            DropIndex("dbo.SongAlbums", new[] { "Song_SongID" });
            DropIndex("dbo.SongAlbums", new[] { "Album_AlbumID" });
            DropIndex("dbo.AppUserAlbums", new[] { "AppUser_Id" });
            DropIndex("dbo.AppUserAlbums", new[] { "Album_AlbumID" });
            DropIndex("dbo.AppUserSongs", new[] { "AppUser_Id" });
            DropIndex("dbo.AppUserSongs", new[] { "Song_SongID" });
            DropPrimaryKey("dbo.CreditCards");
            AddColumn("dbo.Songs", "SongAlbum_AlbumID", c => c.Short());
            AddColumn("dbo.AspNetUsers", "Song_SongID", c => c.Short());
            AddColumn("dbo.AspNetUsers", "Artist_ArtistID", c => c.Short());
            AddColumn("dbo.AspNetUsers", "Album_AlbumID", c => c.Short());
            AddColumn("dbo.CreditCards", "CreditCardNumber", c => c.String(nullable: false));
            AlterColumn("dbo.CreditCards", "CreditCardID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.CreditCards", "CreditCardID");
            CreateIndex("dbo.AspNetUsers", "Song_SongID");
            CreateIndex("dbo.AspNetUsers", "Artist_ArtistID");
            CreateIndex("dbo.AspNetUsers", "Album_AlbumID");
            CreateIndex("dbo.Songs", "SongAlbum_AlbumID");
            AddForeignKey("dbo.Songs", "SongAlbum_AlbumID", "dbo.Albums", "AlbumID");
            AddForeignKey("dbo.AspNetUsers", "Song_SongID", "dbo.Songs", "SongID");
            AddForeignKey("dbo.AspNetUsers", "Artist_ArtistID", "dbo.Artists", "ArtistID");
            AddForeignKey("dbo.AspNetUsers", "Album_AlbumID", "dbo.Albums", "AlbumID");
            DropTable("dbo.SongAlbums");
            DropTable("dbo.AppUserAlbums");
            DropTable("dbo.AppUserSongs");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.AppUserSongs",
                c => new
                    {
                        AppUser_Id = c.String(nullable: false, maxLength: 128),
                        Song_SongID = c.Short(nullable: false),
                    })
                .PrimaryKey(t => new { t.AppUser_Id, t.Song_SongID });
            
            CreateTable(
                "dbo.AppUserAlbums",
                c => new
                    {
                        AppUser_Id = c.String(nullable: false, maxLength: 128),
                        Album_AlbumID = c.Short(nullable: false),
                    })
                .PrimaryKey(t => new { t.AppUser_Id, t.Album_AlbumID });
            
            CreateTable(
                "dbo.SongAlbums",
                c => new
                    {
                        Song_SongID = c.Short(nullable: false),
                        Album_AlbumID = c.Short(nullable: false),
                    })
                .PrimaryKey(t => new { t.Song_SongID, t.Album_AlbumID });
            
            DropForeignKey("dbo.AspNetUsers", "Album_AlbumID", "dbo.Albums");
            DropForeignKey("dbo.AspNetUsers", "Artist_ArtistID", "dbo.Artists");
            DropForeignKey("dbo.AspNetUsers", "Song_SongID", "dbo.Songs");
            DropForeignKey("dbo.Songs", "SongAlbum_AlbumID", "dbo.Albums");
            DropIndex("dbo.Songs", new[] { "SongAlbum_AlbumID" });
            DropIndex("dbo.AspNetUsers", new[] { "Album_AlbumID" });
            DropIndex("dbo.AspNetUsers", new[] { "Artist_ArtistID" });
            DropIndex("dbo.AspNetUsers", new[] { "Song_SongID" });
            DropPrimaryKey("dbo.CreditCards");
            AlterColumn("dbo.CreditCards", "CreditCardID", c => c.Short(nullable: false, identity: true));
            DropColumn("dbo.CreditCards", "CreditCardNumber");
            DropColumn("dbo.AspNetUsers", "Album_AlbumID");
            DropColumn("dbo.AspNetUsers", "Artist_ArtistID");
            DropColumn("dbo.AspNetUsers", "Song_SongID");
            DropColumn("dbo.Songs", "SongAlbum_AlbumID");
            AddPrimaryKey("dbo.CreditCards", "CreditCardID");
            CreateIndex("dbo.AppUserSongs", "Song_SongID");
            CreateIndex("dbo.AppUserSongs", "AppUser_Id");
            CreateIndex("dbo.AppUserAlbums", "Album_AlbumID");
            CreateIndex("dbo.AppUserAlbums", "AppUser_Id");
            CreateIndex("dbo.SongAlbums", "Album_AlbumID");
            CreateIndex("dbo.SongAlbums", "Song_SongID");
            AddForeignKey("dbo.AppUserSongs", "Song_SongID", "dbo.Songs", "SongID", cascadeDelete: true);
            AddForeignKey("dbo.AppUserSongs", "AppUser_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AppUserAlbums", "Album_AlbumID", "dbo.Albums", "AlbumID", cascadeDelete: true);
            AddForeignKey("dbo.AppUserAlbums", "AppUser_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.SongAlbums", "Album_AlbumID", "dbo.Albums", "AlbumID", cascadeDelete: true);
            AddForeignKey("dbo.SongAlbums", "Song_SongID", "dbo.Songs", "SongID", cascadeDelete: true);
        }
    }
}
