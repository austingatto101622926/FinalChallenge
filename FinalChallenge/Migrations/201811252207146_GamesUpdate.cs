namespace FinalChallenge.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GamesUpdate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        GameId = c.Int(nullable: false, identity: true),
                        DateTime = c.DateTime(nullable: false),
                        Venue = c.String(),
                        FeeAmount = c.Double(nullable: false),
                        FeePayee_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.GameId)
                .ForeignKey("dbo.AspNetUsers", t => t.FeePayee_Id)
                .Index(t => t.FeePayee_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Games", "FeePayee_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Games", new[] { "FeePayee_Id" });
            DropTable("dbo.Games");
        }
    }
}
