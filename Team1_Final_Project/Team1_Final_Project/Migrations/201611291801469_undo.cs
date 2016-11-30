namespace Team1_Final_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class undo : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.ShoppingCarts", name: "Id", newName: "ShoppingCartID");
            RenameColumn(table: "dbo.Albums", name: "ShoppingCart_Id", newName: "ShoppingCart_ShoppingCartID");
            RenameColumn(table: "dbo.Songs", name: "ShoppingCart_Id", newName: "ShoppingCart_ShoppingCartID");
            RenameIndex(table: "dbo.Albums", name: "IX_ShoppingCart_Id", newName: "IX_ShoppingCart_ShoppingCartID");
            RenameIndex(table: "dbo.Songs", name: "IX_ShoppingCart_Id", newName: "IX_ShoppingCart_ShoppingCartID");
            RenameIndex(table: "dbo.ShoppingCarts", name: "IX_Id", newName: "IX_ShoppingCartID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.ShoppingCarts", name: "IX_ShoppingCartID", newName: "IX_Id");
            RenameIndex(table: "dbo.Songs", name: "IX_ShoppingCart_ShoppingCartID", newName: "IX_ShoppingCart_Id");
            RenameIndex(table: "dbo.Albums", name: "IX_ShoppingCart_ShoppingCartID", newName: "IX_ShoppingCart_Id");
            RenameColumn(table: "dbo.Songs", name: "ShoppingCart_ShoppingCartID", newName: "ShoppingCart_Id");
            RenameColumn(table: "dbo.Albums", name: "ShoppingCart_ShoppingCartID", newName: "ShoppingCart_Id");
            RenameColumn(table: "dbo.ShoppingCarts", name: "ShoppingCartID", newName: "Id");
        }
    }
}
