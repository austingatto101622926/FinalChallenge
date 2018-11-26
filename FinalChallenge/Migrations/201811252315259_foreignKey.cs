namespace FinalChallenge.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class foreignKey : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Games", name: "FeePayee_Id", newName: "AspNetUserId");
            RenameIndex(table: "dbo.Games", name: "IX_FeePayee_Id", newName: "IX_AspNetUserId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Games", name: "IX_AspNetUserId", newName: "IX_FeePayee_Id");
            RenameColumn(table: "dbo.Games", name: "AspNetUserId", newName: "FeePayee_Id");
        }
    }
}
