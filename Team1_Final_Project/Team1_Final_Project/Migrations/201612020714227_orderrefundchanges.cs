namespace Team1_Final_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class orderrefundchanges : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.SongOrderBridges", name: "Song_SongID", newName: "SongInOrder_SongID");
            RenameColumn(table: "dbo.AlbumOrderBridges", name: "Album_AlbumID", newName: "AlbumInOrder_AlbumID");
            RenameIndex(table: "dbo.AlbumOrderBridges", name: "IX_Album_AlbumID", newName: "IX_AlbumInOrder_AlbumID");
            RenameIndex(table: "dbo.SongOrderBridges", name: "IX_Song_SongID", newName: "IX_SongInOrder_SongID");
            AddColumn("dbo.Orders", "IsGift", c => c.Boolean(nullable: false));
            AddColumn("dbo.Orders", "RecipientID", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "RecipientID");
            DropColumn("dbo.Orders", "IsGift");
            RenameIndex(table: "dbo.SongOrderBridges", name: "IX_SongInOrder_SongID", newName: "IX_Song_SongID");
            RenameIndex(table: "dbo.AlbumOrderBridges", name: "IX_AlbumInOrder_AlbumID", newName: "IX_Album_AlbumID");
            RenameColumn(table: "dbo.AlbumOrderBridges", name: "AlbumInOrder_AlbumID", newName: "Album_AlbumID");
            RenameColumn(table: "dbo.SongOrderBridges", name: "SongInOrder_SongID", newName: "Song_SongID");
        }
    }
}
