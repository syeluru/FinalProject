namespace Team1_Final_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modelchanges : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.MusicRatings", "Review", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.MusicRatings", "Review", c => c.String());
        }
    }
}
