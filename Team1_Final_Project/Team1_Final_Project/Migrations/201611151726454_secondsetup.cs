namespace Team1_Final_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class secondsetup : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Orders", name: "AppUser_Id", newName: "Customer_Id");
            RenameIndex(table: "dbo.Orders", name: "IX_AppUser_Id", newName: "IX_Customer_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Orders", name: "IX_Customer_Id", newName: "IX_AppUser_Id");
            RenameColumn(table: "dbo.Orders", name: "Customer_Id", newName: "AppUser_Id");
        }
    }
}
