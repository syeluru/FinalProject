namespace Team1_Final_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class yooo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Discounts", "DiscountPercentage", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Discounts", "DiscountAmount");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Discounts", "DiscountAmount", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            DropColumn("dbo.Discounts", "DiscountPercentage");
        }
    }
}
