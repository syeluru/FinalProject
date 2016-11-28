namespace Team1_Final_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dan : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "MName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "MName", c => c.String());
        }
    }
}
