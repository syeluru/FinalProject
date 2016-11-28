namespace Team1_Final_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixedsomeerrors : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "SSN", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "SSN", c => c.String(nullable: false));
        }
    }
}
