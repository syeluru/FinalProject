namespace Team1_Final_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class employeemodelchange : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "IsActive", c => c.Boolean(nullable: false));
            AlterColumn("dbo.MusicRatings", "Review", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MusicRatings", "Review", c => c.String(maxLength: 100));
            DropColumn("dbo.AspNetUsers", "IsActive");
        }
    }
}
