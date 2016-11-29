namespace Team1_Final_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fuckmeagain : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.ShoppingCarts", name: "ShoppingCartID", newName: "CustomerID");
            RenameColumn(table: "dbo.Albums", name: "ShoppingCart_ShoppingCartID", newName: "ShoppingCart_CustomerID");
            RenameColumn(table: "dbo.Songs", name: "ShoppingCart_ShoppingCartID", newName: "ShoppingCart_CustomerID");
            RenameIndex(table: "dbo.Albums", name: "IX_ShoppingCart_ShoppingCartID", newName: "IX_ShoppingCart_CustomerID");
            RenameIndex(table: "dbo.Songs", name: "IX_ShoppingCart_ShoppingCartID", newName: "IX_ShoppingCart_CustomerID");
            RenameIndex(table: "dbo.ShoppingCarts", name: "IX_ShoppingCartID", newName: "IX_CustomerID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.ShoppingCarts", name: "IX_CustomerID", newName: "IX_ShoppingCartID");
            RenameIndex(table: "dbo.Songs", name: "IX_ShoppingCart_CustomerID", newName: "IX_ShoppingCart_ShoppingCartID");
            RenameIndex(table: "dbo.Albums", name: "IX_ShoppingCart_CustomerID", newName: "IX_ShoppingCart_ShoppingCartID");
            RenameColumn(table: "dbo.Songs", name: "ShoppingCart_CustomerID", newName: "ShoppingCart_ShoppingCartID");
            RenameColumn(table: "dbo.Albums", name: "ShoppingCart_CustomerID", newName: "ShoppingCart_ShoppingCartID");
            RenameColumn(table: "dbo.ShoppingCarts", name: "CustomerID", newName: "ShoppingCartID");
        }
    }
}
