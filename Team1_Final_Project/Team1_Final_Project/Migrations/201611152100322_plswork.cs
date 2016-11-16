namespace Team1_Final_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class plswork : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Artists", "ArtistPrice");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Artists", "ArtistPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
