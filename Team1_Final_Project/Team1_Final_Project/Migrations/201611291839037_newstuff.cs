namespace Team1_Final_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newstuff : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "MName", c => c.String(maxLength: 1));
            AlterColumn("dbo.AspNetUsers", "SSN", c => c.String(maxLength: 9));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "SSN", c => c.String());
            AlterColumn("dbo.AspNetUsers", "MName", c => c.String());
        }
    }
}
