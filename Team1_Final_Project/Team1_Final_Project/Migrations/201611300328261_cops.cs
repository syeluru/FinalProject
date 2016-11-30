namespace Team1_Final_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cops : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ShoppingCartAlbums", "ShoppingCart_ShoppingCartID", "dbo.ShoppingCarts");
            DropForeignKey("dbo.ShoppingCartAlbums", "Album_AlbumID", "dbo.Albums");
            DropForeignKey("dbo.ShoppingCarts", "Customer_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ShoppingCartSongs", "ShoppingCart_ShoppingCartID", "dbo.ShoppingCarts");
            DropForeignKey("dbo.ShoppingCartSongs", "Song_SongID", "dbo.Songs");
            DropIndex("dbo.ShoppingCarts", new[] { "Customer_Id" });
            DropIndex("dbo.ShoppingCartAlbums", new[] { "ShoppingCart_ShoppingCartID" });
            DropIndex("dbo.ShoppingCartAlbums", new[] { "Album_AlbumID" });
            DropIndex("dbo.ShoppingCartSongs", new[] { "ShoppingCart_ShoppingCartID" });
            DropIndex("dbo.ShoppingCartSongs", new[] { "Song_SongID" });
            CreateTable(
                "dbo.AlbumInShoppingCarts",
                c => new
                    {
                        AlbumInShoppingCartID = c.Int(nullable: false, identity: true),
                        Album_AlbumID = c.Short(),
                        Customer_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.AlbumInShoppingCartID)
                .ForeignKey("dbo.Albums", t => t.Album_AlbumID)
                .ForeignKey("dbo.AspNetUsers", t => t.Customer_Id)
                .Index(t => t.Album_AlbumID)
                .Index(t => t.Customer_Id);
            
            CreateTable(
                "dbo.SongInShoppingCarts",
                c => new
                    {
                        SongInShoppingCartID = c.Int(nullable: false, identity: true),
                        Customer_Id = c.String(maxLength: 128),
                        Song_SongID = c.Short(),
                    })
                .PrimaryKey(t => t.SongInShoppingCartID)
                .ForeignKey("dbo.AspNetUsers", t => t.Customer_Id)
                .ForeignKey("dbo.Songs", t => t.Song_SongID)
                .Index(t => t.Customer_Id)
                .Index(t => t.Song_SongID);
            
            DropTable("dbo.ShoppingCarts");
            DropTable("dbo.ShoppingCartAlbums");
            DropTable("dbo.ShoppingCartSongs");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ShoppingCartSongs",
                c => new
                    {
                        ShoppingCart_ShoppingCartID = c.Int(nullable: false),
                        Song_SongID = c.Short(nullable: false),
                    })
                .PrimaryKey(t => new { t.ShoppingCart_ShoppingCartID, t.Song_SongID });
            
            CreateTable(
                "dbo.ShoppingCartAlbums",
                c => new
                    {
                        ShoppingCart_ShoppingCartID = c.Int(nullable: false),
                        Album_AlbumID = c.Short(nullable: false),
                    })
                .PrimaryKey(t => new { t.ShoppingCart_ShoppingCartID, t.Album_AlbumID });
            
            CreateTable(
                "dbo.ShoppingCarts",
                c => new
                    {
                        ShoppingCartID = c.Int(nullable: false, identity: true),
                        TotalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Customer_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ShoppingCartID);
            
            DropForeignKey("dbo.SongInShoppingCarts", "Song_SongID", "dbo.Songs");
            DropForeignKey("dbo.SongInShoppingCarts", "Customer_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AlbumInShoppingCarts", "Customer_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AlbumInShoppingCarts", "Album_AlbumID", "dbo.Albums");
            DropIndex("dbo.SongInShoppingCarts", new[] { "Song_SongID" });
            DropIndex("dbo.SongInShoppingCarts", new[] { "Customer_Id" });
            DropIndex("dbo.AlbumInShoppingCarts", new[] { "Customer_Id" });
            DropIndex("dbo.AlbumInShoppingCarts", new[] { "Album_AlbumID" });
            DropTable("dbo.SongInShoppingCarts");
            DropTable("dbo.AlbumInShoppingCarts");
            CreateIndex("dbo.ShoppingCartSongs", "Song_SongID");
            CreateIndex("dbo.ShoppingCartSongs", "ShoppingCart_ShoppingCartID");
            CreateIndex("dbo.ShoppingCartAlbums", "Album_AlbumID");
            CreateIndex("dbo.ShoppingCartAlbums", "ShoppingCart_ShoppingCartID");
            CreateIndex("dbo.ShoppingCarts", "Customer_Id");
            AddForeignKey("dbo.ShoppingCartSongs", "Song_SongID", "dbo.Songs", "SongID", cascadeDelete: true);
            AddForeignKey("dbo.ShoppingCartSongs", "ShoppingCart_ShoppingCartID", "dbo.ShoppingCarts", "ShoppingCartID", cascadeDelete: true);
            AddForeignKey("dbo.ShoppingCarts", "Customer_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.ShoppingCartAlbums", "Album_AlbumID", "dbo.Albums", "AlbumID", cascadeDelete: true);
            AddForeignKey("dbo.ShoppingCartAlbums", "ShoppingCart_ShoppingCartID", "dbo.ShoppingCarts", "ShoppingCartID", cascadeDelete: true);
        }
    }
}
