namespace Team1_Final_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangingCCDatatype : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.CreditCards");
            AddColumn("dbo.CreditCards", "CreditCardNumber", c => c.String(nullable: false));
            AlterColumn("dbo.CreditCards", "CreditCardID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.CreditCards", "CreditCardID");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.CreditCards");
            AlterColumn("dbo.CreditCards", "CreditCardID", c => c.Short(nullable: false, identity: true));
            DropColumn("dbo.CreditCards", "CreditCardNumber");
            AddPrimaryKey("dbo.CreditCards", "CreditCardID");
        }
    }
}
