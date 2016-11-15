namespace Team1_Final_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sixth1 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.GenreSongs", newName: "SongGenres");
            DropPrimaryKey("dbo.SongGenres");
            AddPrimaryKey("dbo.SongGenres", new[] { "Song_SongID", "Genre_GenreID" });
            DropColumn("dbo.Artists", "ArtistPrice");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Artists", "ArtistPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropPrimaryKey("dbo.SongGenres");
            AddPrimaryKey("dbo.SongGenres", new[] { "Genre_GenreID", "Song_SongID" });
            RenameTable(name: "dbo.SongGenres", newName: "GenreSongs");
        }
    }
}
