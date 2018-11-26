namespace FinalChallenge.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "TotalFeesPaid", c => c.Double());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "TotalFeesPaid", c => c.Double(nullable: false));
        }
    }
}
