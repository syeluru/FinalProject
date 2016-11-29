namespace Team1_Final_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class please : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Albums", "ShoppingCart_ShoppingCartID", "dbo.ShoppingCarts");
            DropForeignKey("dbo.Songs", "ShoppingCart_ShoppingCartID", "dbo.ShoppingCarts");
            DropForeignKey("dbo.ShoppingCarts", "ShoppingCartID", "dbo.AspNetUsers");
            DropIndex("dbo.Albums", new[] { "ShoppingCart_ShoppingCartID" });
            DropIndex("dbo.Songs", new[] { "ShoppingCart_ShoppingCartID" });
            DropIndex("dbo.ShoppingCarts", new[] { "ShoppingCartID" });
            DropPrimaryKey("dbo.ShoppingCarts");
            CreateTable(
                "dbo.ShoppingCartAlbums",
                c => new
                    {
                        ShoppingCart_ShoppingCartID = c.Int(nullable: false),
                        Album_AlbumID = c.Short(nullable: false),
                    })
                .PrimaryKey(t => new { t.ShoppingCart_ShoppingCartID, t.Album_AlbumID })
                .ForeignKey("dbo.ShoppingCarts", t => t.ShoppingCart_ShoppingCartID, cascadeDelete: true)
                .ForeignKey("dbo.Albums", t => t.Album_AlbumID, cascadeDelete: true)
                .Index(t => t.ShoppingCart_ShoppingCartID)
                .Index(t => t.Album_AlbumID);
            
            CreateTable(
                "dbo.ShoppingCartSongs",
                c => new
                    {
                        ShoppingCart_ShoppingCartID = c.Int(nullable: false),
                        Song_SongID = c.Short(nullable: false),
                    })
                .PrimaryKey(t => new { t.ShoppingCart_ShoppingCartID, t.Song_SongID })
                .ForeignKey("dbo.ShoppingCarts", t => t.ShoppingCart_ShoppingCartID, cascadeDelete: true)
                .ForeignKey("dbo.Songs", t => t.Song_SongID, cascadeDelete: true)
                .Index(t => t.ShoppingCart_ShoppingCartID)
                .Index(t => t.Song_SongID);
            
            AddColumn("dbo.ShoppingCarts", "Customer_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.ShoppingCarts", "ShoppingCartID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.ShoppingCarts", "ShoppingCartID");
            CreateIndex("dbo.ShoppingCarts", "Customer_Id");
            AddForeignKey("dbo.ShoppingCarts", "Customer_Id", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.Albums", "ShoppingCart_ShoppingCartID");
            DropColumn("dbo.Songs", "ShoppingCart_ShoppingCartID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Songs", "ShoppingCart_ShoppingCartID", c => c.String(maxLength: 128));
            AddColumn("dbo.Albums", "ShoppingCart_ShoppingCartID", c => c.String(maxLength: 128));
            DropForeignKey("dbo.ShoppingCarts", "Customer_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ShoppingCartSongs", "Song_SongID", "dbo.Songs");
            DropForeignKey("dbo.ShoppingCartSongs", "ShoppingCart_ShoppingCartID", "dbo.ShoppingCarts");
            DropForeignKey("dbo.ShoppingCartAlbums", "Album_AlbumID", "dbo.Albums");
            DropForeignKey("dbo.ShoppingCartAlbums", "ShoppingCart_ShoppingCartID", "dbo.ShoppingCarts");
            DropIndex("dbo.ShoppingCartSongs", new[] { "Song_SongID" });
            DropIndex("dbo.ShoppingCartSongs", new[] { "ShoppingCart_ShoppingCartID" });
            DropIndex("dbo.ShoppingCartAlbums", new[] { "Album_AlbumID" });
            DropIndex("dbo.ShoppingCartAlbums", new[] { "ShoppingCart_ShoppingCartID" });
            DropIndex("dbo.ShoppingCarts", new[] { "Customer_Id" });
            DropPrimaryKey("dbo.ShoppingCarts");
            AlterColumn("dbo.ShoppingCarts", "ShoppingCartID", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.ShoppingCarts", "Customer_Id");
            DropTable("dbo.ShoppingCartSongs");
            DropTable("dbo.ShoppingCartAlbums");
            AddPrimaryKey("dbo.ShoppingCarts", "ShoppingCartID");
            CreateIndex("dbo.ShoppingCarts", "ShoppingCartID");
            CreateIndex("dbo.Songs", "ShoppingCart_ShoppingCartID");
            CreateIndex("dbo.Albums", "ShoppingCart_ShoppingCartID");
            AddForeignKey("dbo.ShoppingCarts", "ShoppingCartID", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Songs", "ShoppingCart_ShoppingCartID", "dbo.ShoppingCarts", "ShoppingCartID");
            AddForeignKey("dbo.Albums", "ShoppingCart_ShoppingCartID", "dbo.ShoppingCarts", "ShoppingCartID");
        }
    }
}
