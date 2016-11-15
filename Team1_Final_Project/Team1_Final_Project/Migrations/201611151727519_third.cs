namespace Team1_Final_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class third : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.SongGenres", newName: "GenreSongs");
            DropPrimaryKey("dbo.GenreSongs");
            AddColumn("dbo.AlbumOrderBridges", "Album_AlbumID", c => c.Short());
            AddColumn("dbo.SongOrderBridges", "Song_SongID", c => c.Short());
            AddColumn("dbo.Artists", "ArtistPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddPrimaryKey("dbo.GenreSongs", new[] { "Genre_GenreID", "Song_SongID" });
            CreateIndex("dbo.AlbumOrderBridges", "Album_AlbumID");
            CreateIndex("dbo.SongOrderBridges", "Song_SongID");
            AddForeignKey("dbo.AlbumOrderBridges", "Album_AlbumID", "dbo.Albums", "AlbumID");
            AddForeignKey("dbo.SongOrderBridges", "Song_SongID", "dbo.Songs", "SongID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SongOrderBridges", "Song_SongID", "dbo.Songs");
            DropForeignKey("dbo.AlbumOrderBridges", "Album_AlbumID", "dbo.Albums");
            DropIndex("dbo.SongOrderBridges", new[] { "Song_SongID" });
            DropIndex("dbo.AlbumOrderBridges", new[] { "Album_AlbumID" });
            DropPrimaryKey("dbo.GenreSongs");
            DropColumn("dbo.Artists", "ArtistPrice");
            DropColumn("dbo.SongOrderBridges", "Song_SongID");
            DropColumn("dbo.AlbumOrderBridges", "Album_AlbumID");
            AddPrimaryKey("dbo.GenreSongs", new[] { "Song_SongID", "Genre_GenreID" });
            RenameTable(name: "dbo.GenreSongs", newName: "SongGenres");
        }
    }
}
