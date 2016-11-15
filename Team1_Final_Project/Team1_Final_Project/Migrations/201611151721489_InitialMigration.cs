namespace Team1_Final_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FName = c.String(nullable: false),
                        LName = c.String(nullable: false),
                        StreetAddress = c.String(nullable: false),
                        City = c.String(nullable: false),
                        State = c.String(nullable: false),
                        ZipCode = c.String(nullable: false),
                        UserType = c.Int(nullable: false),
                        IsAccountEnabled = c.Boolean(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        Song_SongID = c.Short(),
                        Artist_ArtistID = c.Short(),
                        Album_AlbumID = c.Short(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Songs", t => t.Song_SongID)
                .ForeignKey("dbo.Artists", t => t.Artist_ArtistID)
                .ForeignKey("dbo.Albums", t => t.Album_AlbumID)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.Song_SongID)
                .Index(t => t.Artist_ArtistID)
                .Index(t => t.Album_AlbumID);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.CreditCards",
                c => new
                    {
                        CreditCardID = c.Short(nullable: false, identity: true),
                        CreditCardType = c.Int(nullable: false),
                        Person_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.CreditCardID)
                .ForeignKey("dbo.AspNetUsers", t => t.Person_Id)
                .Index(t => t.Person_Id);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        OrderID = c.Short(nullable: false, identity: true),
                        TotalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AppUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.OrderID)
                .ForeignKey("dbo.AspNetUsers", t => t.AppUser_Id)
                .Index(t => t.AppUser_Id);
            
            CreateTable(
                "dbo.AlbumOrderBridges",
                c => new
                    {
                        AlbumOrderBridgeID = c.Int(nullable: false, identity: true),
                        Order_OrderID = c.Short(),
                    })
                .PrimaryKey(t => t.AlbumOrderBridgeID)
                .ForeignKey("dbo.Orders", t => t.Order_OrderID)
                .Index(t => t.Order_OrderID);
            
            CreateTable(
                "dbo.SongOrderBridges",
                c => new
                    {
                        SongOrderBridgeID = c.Int(nullable: false, identity: true),
                        Order_OrderID = c.Short(),
                    })
                .PrimaryKey(t => t.SongOrderBridgeID)
                .ForeignKey("dbo.Orders", t => t.Order_OrderID)
                .Index(t => t.Order_OrderID);
            
            CreateTable(
                "dbo.ShoppingCarts",
                c => new
                    {
                        ShoppingCartID = c.String(nullable: false, maxLength: 128),
                        TotalPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ShoppingCartID)
                .ForeignKey("dbo.AspNetUsers", t => t.ShoppingCartID)
                .Index(t => t.ShoppingCartID);
            
            CreateTable(
                "dbo.Albums",
                c => new
                    {
                        AlbumID = c.Short(nullable: false, identity: true),
                        AlbumName = c.String(nullable: false),
                        AlbumPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AlbumDiscount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ShoppingCart_ShoppingCartID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.AlbumID)
                .ForeignKey("dbo.ShoppingCarts", t => t.ShoppingCart_ShoppingCartID)
                .Index(t => t.ShoppingCart_ShoppingCartID);
            
            CreateTable(
                "dbo.Artists",
                c => new
                    {
                        ArtistID = c.Short(nullable: false, identity: true),
                        ArtistName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ArtistID);
            
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        GenreID = c.Short(nullable: false, identity: true),
                        GenreName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.GenreID);
            
            CreateTable(
                "dbo.Songs",
                c => new
                    {
                        SongID = c.Short(nullable: false, identity: true),
                        SongName = c.String(nullable: false),
                        SongPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SongDiscount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SongAlbum_AlbumID = c.Short(),
                        ShoppingCart_ShoppingCartID = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.SongID)
                .ForeignKey("dbo.Albums", t => t.SongAlbum_AlbumID)
                .ForeignKey("dbo.ShoppingCarts", t => t.ShoppingCart_ShoppingCartID)
                .Index(t => t.SongAlbum_AlbumID)
                .Index(t => t.ShoppingCart_ShoppingCartID);
            
            CreateTable(
                "dbo.ArtistAlbums",
                c => new
                    {
                        Artist_ArtistID = c.Short(nullable: false),
                        Album_AlbumID = c.Short(nullable: false),
                    })
                .PrimaryKey(t => new { t.Artist_ArtistID, t.Album_AlbumID })
                .ForeignKey("dbo.Artists", t => t.Artist_ArtistID, cascadeDelete: true)
                .ForeignKey("dbo.Albums", t => t.Album_AlbumID, cascadeDelete: true)
                .Index(t => t.Artist_ArtistID)
                .Index(t => t.Album_AlbumID);
            
            CreateTable(
                "dbo.GenreAlbums",
                c => new
                    {
                        Genre_GenreID = c.Short(nullable: false),
                        Album_AlbumID = c.Short(nullable: false),
                    })
                .PrimaryKey(t => new { t.Genre_GenreID, t.Album_AlbumID })
                .ForeignKey("dbo.Genres", t => t.Genre_GenreID, cascadeDelete: true)
                .ForeignKey("dbo.Albums", t => t.Album_AlbumID, cascadeDelete: true)
                .Index(t => t.Genre_GenreID)
                .Index(t => t.Album_AlbumID);
            
            CreateTable(
                "dbo.GenreArtists",
                c => new
                    {
                        Genre_GenreID = c.Short(nullable: false),
                        Artist_ArtistID = c.Short(nullable: false),
                    })
                .PrimaryKey(t => new { t.Genre_GenreID, t.Artist_ArtistID })
                .ForeignKey("dbo.Genres", t => t.Genre_GenreID, cascadeDelete: true)
                .ForeignKey("dbo.Artists", t => t.Artist_ArtistID, cascadeDelete: true)
                .Index(t => t.Genre_GenreID)
                .Index(t => t.Artist_ArtistID);
            
            CreateTable(
                "dbo.SongArtists",
                c => new
                    {
                        Song_SongID = c.Short(nullable: false),
                        Artist_ArtistID = c.Short(nullable: false),
                    })
                .PrimaryKey(t => new { t.Song_SongID, t.Artist_ArtistID })
                .ForeignKey("dbo.Songs", t => t.Song_SongID, cascadeDelete: true)
                .ForeignKey("dbo.Artists", t => t.Artist_ArtistID, cascadeDelete: true)
                .Index(t => t.Song_SongID)
                .Index(t => t.Artist_ArtistID);
            
            CreateTable(
                "dbo.SongGenres",
                c => new
                    {
                        Song_SongID = c.Short(nullable: false),
                        Genre_GenreID = c.Short(nullable: false),
                    })
                .PrimaryKey(t => new { t.Song_SongID, t.Genre_GenreID })
                .ForeignKey("dbo.Songs", t => t.Song_SongID, cascadeDelete: true)
                .ForeignKey("dbo.Genres", t => t.Genre_GenreID, cascadeDelete: true)
                .Index(t => t.Song_SongID)
                .Index(t => t.Genre_GenreID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Songs", "ShoppingCart_ShoppingCartID", "dbo.ShoppingCarts");
            DropForeignKey("dbo.ShoppingCarts", "ShoppingCartID", "dbo.AspNetUsers");
            DropForeignKey("dbo.Albums", "ShoppingCart_ShoppingCartID", "dbo.ShoppingCarts");
            DropForeignKey("dbo.AspNetUsers", "Album_AlbumID", "dbo.Albums");
            DropForeignKey("dbo.AspNetUsers", "Artist_ArtistID", "dbo.Artists");
            DropForeignKey("dbo.AspNetUsers", "Song_SongID", "dbo.Songs");
            DropForeignKey("dbo.SongGenres", "Genre_GenreID", "dbo.Genres");
            DropForeignKey("dbo.SongGenres", "Song_SongID", "dbo.Songs");
            DropForeignKey("dbo.SongArtists", "Artist_ArtistID", "dbo.Artists");
            DropForeignKey("dbo.SongArtists", "Song_SongID", "dbo.Songs");
            DropForeignKey("dbo.Songs", "SongAlbum_AlbumID", "dbo.Albums");
            DropForeignKey("dbo.GenreArtists", "Artist_ArtistID", "dbo.Artists");
            DropForeignKey("dbo.GenreArtists", "Genre_GenreID", "dbo.Genres");
            DropForeignKey("dbo.GenreAlbums", "Album_AlbumID", "dbo.Albums");
            DropForeignKey("dbo.GenreAlbums", "Genre_GenreID", "dbo.Genres");
            DropForeignKey("dbo.ArtistAlbums", "Album_AlbumID", "dbo.Albums");
            DropForeignKey("dbo.ArtistAlbums", "Artist_ArtistID", "dbo.Artists");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Orders", "AppUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.SongOrderBridges", "Order_OrderID", "dbo.Orders");
            DropForeignKey("dbo.AlbumOrderBridges", "Order_OrderID", "dbo.Orders");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.CreditCards", "Person_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropIndex("dbo.SongGenres", new[] { "Genre_GenreID" });
            DropIndex("dbo.SongGenres", new[] { "Song_SongID" });
            DropIndex("dbo.SongArtists", new[] { "Artist_ArtistID" });
            DropIndex("dbo.SongArtists", new[] { "Song_SongID" });
            DropIndex("dbo.GenreArtists", new[] { "Artist_ArtistID" });
            DropIndex("dbo.GenreArtists", new[] { "Genre_GenreID" });
            DropIndex("dbo.GenreAlbums", new[] { "Album_AlbumID" });
            DropIndex("dbo.GenreAlbums", new[] { "Genre_GenreID" });
            DropIndex("dbo.ArtistAlbums", new[] { "Album_AlbumID" });
            DropIndex("dbo.ArtistAlbums", new[] { "Artist_ArtistID" });
            DropIndex("dbo.Songs", new[] { "ShoppingCart_ShoppingCartID" });
            DropIndex("dbo.Songs", new[] { "SongAlbum_AlbumID" });
            DropIndex("dbo.Albums", new[] { "ShoppingCart_ShoppingCartID" });
            DropIndex("dbo.ShoppingCarts", new[] { "ShoppingCartID" });
            DropIndex("dbo.SongOrderBridges", new[] { "Order_OrderID" });
            DropIndex("dbo.AlbumOrderBridges", new[] { "Order_OrderID" });
            DropIndex("dbo.Orders", new[] { "AppUser_Id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.CreditCards", new[] { "Person_Id" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", new[] { "Album_AlbumID" });
            DropIndex("dbo.AspNetUsers", new[] { "Artist_ArtistID" });
            DropIndex("dbo.AspNetUsers", new[] { "Song_SongID" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropTable("dbo.SongGenres");
            DropTable("dbo.SongArtists");
            DropTable("dbo.GenreArtists");
            DropTable("dbo.GenreAlbums");
            DropTable("dbo.ArtistAlbums");
            DropTable("dbo.Songs");
            DropTable("dbo.Genres");
            DropTable("dbo.Artists");
            DropTable("dbo.Albums");
            DropTable("dbo.ShoppingCarts");
            DropTable("dbo.SongOrderBridges");
            DropTable("dbo.AlbumOrderBridges");
            DropTable("dbo.Orders");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.CreditCards");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
        }
    }
}
