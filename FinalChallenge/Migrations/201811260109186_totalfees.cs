namespace FinalChallenge.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class totalfees : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "TotalFeesPaid", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "TotalFeesPaid");
        }
    }
}
