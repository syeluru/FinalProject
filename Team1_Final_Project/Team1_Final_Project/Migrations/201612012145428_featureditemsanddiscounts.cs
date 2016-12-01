namespace Team1_Final_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class featureditemsanddiscounts : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Albums", "AlbumDiscount");
            DropColumn("dbo.Songs", "SongDiscount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Songs", "SongDiscount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Albums", "AlbumDiscount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
