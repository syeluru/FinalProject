namespace Team1_Final_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class uptodateasofnov27 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "MName", c => c.String());
            AddColumn("dbo.AspNetUsers", "SSN", c => c.String(nullable: false));
            AddColumn("dbo.AspNetUsers", "EmpType", c => c.Short(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "EmpType");
            DropColumn("dbo.AspNetUsers", "SSN");
            DropColumn("dbo.AspNetUsers", "MName");
        }
    }
}
