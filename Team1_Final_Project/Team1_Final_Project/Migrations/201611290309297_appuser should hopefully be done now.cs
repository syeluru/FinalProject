namespace Team1_Final_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class appusershouldhopefullybedonenow : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MusicRatings", "Customer_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.MusicRatings", "Customer_Id");
            AddForeignKey("dbo.MusicRatings", "Customer_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MusicRatings", "Customer_Id", "dbo.AspNetUsers");
            DropIndex("dbo.MusicRatings", new[] { "Customer_Id" });
            DropColumn("dbo.MusicRatings", "Customer_Id");
        }
    }
}
