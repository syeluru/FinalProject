namespace Team1_Final_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hopefullythisworks : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "MName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "MName");
        }
    }
}
