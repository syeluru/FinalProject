namespace Team1_Final_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class testingwithmeghan : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.CreditCards");
            AlterColumn("dbo.CreditCards", "CreditCardID", c => c.Short(nullable: false, identity: true));
            AddPrimaryKey("dbo.CreditCards", "CreditCardID");
            DropColumn("dbo.CreditCards", "CreditCardNumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CreditCards", "CreditCardNumber", c => c.String(nullable: false));
            DropPrimaryKey("dbo.CreditCards");
            AlterColumn("dbo.CreditCards", "CreditCardID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.CreditCards", "CreditCardID");
        }
    }
}
