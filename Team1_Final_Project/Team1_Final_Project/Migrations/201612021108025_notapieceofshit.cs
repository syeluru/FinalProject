namespace Team1_Final_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class notapieceofshit : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Songs", "AverageSongRating");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Songs", "AverageSongRating", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
