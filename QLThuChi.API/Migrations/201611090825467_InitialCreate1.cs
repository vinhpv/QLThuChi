namespace QLThuChi.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate1 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Thuchis", name: "UserId", newName: "User_Id");
            RenameIndex(table: "dbo.Thuchis", name: "IX_UserId", newName: "IX_User_Id");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Thuchis", name: "IX_User_Id", newName: "IX_UserId");
            RenameColumn(table: "dbo.Thuchis", name: "User_Id", newName: "UserId");
        }
    }
}
