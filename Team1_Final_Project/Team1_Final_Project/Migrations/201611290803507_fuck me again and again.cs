namespace Team1_Final_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fuckmeagainandagain : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.ShoppingCarts", name: "CustomerID", newName: "Id");
            RenameColumn(table: "dbo.Albums", name: "ShoppingCart_CustomerID", newName: "ShoppingCart_Id");
            RenameColumn(table: "dbo.Songs", name: "ShoppingCart_CustomerID", newName: "ShoppingCart_Id");
            RenameIndex(table: "dbo.Albums", name: "IX_ShoppingCart_CustomerID", newName: "IX_ShoppingCart_Id");
            RenameIndex(table: "dbo.Songs", name: "IX_ShoppingCart_CustomerID", newName: "IX_ShoppingCart_Id");
            RenameIndex(table: "dbo.ShoppingCarts", name: "IX_CustomerID", newName: "IX_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.ShoppingCarts", name: "IX_Id", newName: "IX_CustomerID");
            RenameIndex(table: "dbo.Songs", name: "IX_ShoppingCart_Id", newName: "IX_ShoppingCart_CustomerID");
            RenameIndex(table: "dbo.Albums", name: "IX_ShoppingCart_Id", newName: "IX_ShoppingCart_CustomerID");
            RenameColumn(table: "dbo.Songs", name: "ShoppingCart_Id", newName: "ShoppingCart_CustomerID");
            RenameColumn(table: "dbo.Albums", name: "ShoppingCart_Id", newName: "ShoppingCart_CustomerID");
            RenameColumn(table: "dbo.ShoppingCarts", name: "Id", newName: "CustomerID");
        }
    }
}
