namespace Team1_Final_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class averagestuff : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Songs", "SongAverage", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Songs", "SongAverage");
        }
    }
}
