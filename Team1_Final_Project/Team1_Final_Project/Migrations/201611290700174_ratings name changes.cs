namespace Team1_Final_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ratingsnamechanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "CreditCardUsed_CreditCardID", c => c.Int());
            CreateIndex("dbo.Orders", "CreditCardUsed_CreditCardID");
            AddForeignKey("dbo.Orders", "CreditCardUsed_CreditCardID", "dbo.CreditCards", "CreditCardID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "CreditCardUsed_CreditCardID", "dbo.CreditCards");
            DropIndex("dbo.Orders", new[] { "CreditCardUsed_CreditCardID" });
            DropColumn("dbo.Orders", "CreditCardUsed_CreditCardID");
        }
    }
}
