namespace Team1_Final_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class yeeeeeeeeeeeeeee : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Songs", "AverageSongRating", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Discounts", "DiscountAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Discounts", "DiscountPercentage");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Discounts", "DiscountPercentage", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Discounts", "DiscountAmount");
            DropColumn("dbo.Songs", "AverageSongRating");
        }
    }
}
