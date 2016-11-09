namespace QLThuChi.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Thuchis", "User_Id", "dbo.Users");
            DropIndex("dbo.Thuchis", new[] { "User_Id" });
            AddColumn("dbo.Lydoes", "KieuThu", c => c.Int(nullable: false));
            AlterColumn("dbo.Thuchis", "User_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.Thuchis", "User_Id");
            AddForeignKey("dbo.Thuchis", "User_Id", "dbo.Users", "Id");
            DropColumn("dbo.Thuchis", "KieuThu");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Thuchis", "KieuThu", c => c.Boolean());
            DropForeignKey("dbo.Thuchis", "User_Id", "dbo.Users");
            DropIndex("dbo.Thuchis", new[] { "User_Id" });
            AlterColumn("dbo.Thuchis", "User_Id", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.Lydoes", "KieuThu");
            CreateIndex("dbo.Thuchis", "User_Id");
            AddForeignKey("dbo.Thuchis", "User_Id", "dbo.Users", "Id", cascadeDelete: true);
        }
    }
}
