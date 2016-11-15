namespace Team1_Final_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fourth : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.GenreSongs", newName: "SongGenres");
            DropForeignKey("dbo.AlbumOrderBridges", "Album_AlbumID", "dbo.Albums");
            DropForeignKey("dbo.SongOrderBridges", "Song_SongID", "dbo.Songs");
            DropIndex("dbo.AlbumOrderBridges", new[] { "Album_AlbumID" });
            DropIndex("dbo.SongOrderBridges", new[] { "Song_SongID" });
            DropPrimaryKey("dbo.SongGenres");
            AddPrimaryKey("dbo.SongGenres", new[] { "Song_SongID", "Genre_GenreID" });
            DropColumn("dbo.AlbumOrderBridges", "Album_AlbumID");
            DropColumn("dbo.Artists", "ArtistPrice");
            DropColumn("dbo.SongOrderBridges", "Song_SongID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SongOrderBridges", "Song_SongID", c => c.Short());
            AddColumn("dbo.Artists", "ArtistPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.AlbumOrderBridges", "Album_AlbumID", c => c.Short());
            DropPrimaryKey("dbo.SongGenres");
            AddPrimaryKey("dbo.SongGenres", new[] { "Genre_GenreID", "Song_SongID" });
            CreateIndex("dbo.SongOrderBridges", "Song_SongID");
            CreateIndex("dbo.AlbumOrderBridges", "Album_AlbumID");
            AddForeignKey("dbo.SongOrderBridges", "Song_SongID", "dbo.Songs", "SongID");
            AddForeignKey("dbo.AlbumOrderBridges", "Album_AlbumID", "dbo.Albums", "AlbumID");
            RenameTable(name: "dbo.SongGenres", newName: "GenreSongs");
        }
    }
}
