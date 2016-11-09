namespace QLThuChi.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeSecond : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Thuchis", "Lydo_LydoId", "dbo.Lydoes");
            DropForeignKey("dbo.Thuchis", "NguoiThuchi_NguoiThuchiId", "dbo.Nguoithuchis");
            DropIndex("dbo.Thuchis", new[] { "Lydo_LydoId" });
            DropIndex("dbo.Thuchis", new[] { "NguoiThuchi_NguoiThuchiId" });
            RenameColumn(table: "dbo.Thuchis", name: "Lydo_LydoId", newName: "LydoId");
            RenameColumn(table: "dbo.Thuchis", name: "NguoiThuchi_NguoiThuchiId", newName: "NguoithuchiId");
            RenameColumn(table: "dbo.Thuchis", name: "User_Id", newName: "UserId");
            RenameIndex(table: "dbo.Thuchis", name: "IX_User_Id", newName: "IX_UserId");
            AlterColumn("dbo.Thuchis", "LydoId", c => c.Int(nullable: false));
            AlterColumn("dbo.Thuchis", "NguoithuchiId", c => c.Int(nullable: false));
            CreateIndex("dbo.Thuchis", "NguoithuchiId");
            CreateIndex("dbo.Thuchis", "LydoId");
            AddForeignKey("dbo.Thuchis", "LydoId", "dbo.Lydoes", "LydoId", cascadeDelete: true);
            AddForeignKey("dbo.Thuchis", "NguoithuchiId", "dbo.Nguoithuchis", "NguoiThuchiId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Thuchis", "NguoithuchiId", "dbo.Nguoithuchis");
            DropForeignKey("dbo.Thuchis", "LydoId", "dbo.Lydoes");
            DropIndex("dbo.Thuchis", new[] { "LydoId" });
            DropIndex("dbo.Thuchis", new[] { "NguoithuchiId" });
            AlterColumn("dbo.Thuchis", "NguoithuchiId", c => c.Int());
            AlterColumn("dbo.Thuchis", "LydoId", c => c.Int());
            RenameIndex(table: "dbo.Thuchis", name: "IX_UserId", newName: "IX_User_Id");
            RenameColumn(table: "dbo.Thuchis", name: "UserId", newName: "User_Id");
            RenameColumn(table: "dbo.Thuchis", name: "NguoithuchiId", newName: "NguoiThuchi_NguoiThuchiId");
            RenameColumn(table: "dbo.Thuchis", name: "LydoId", newName: "Lydo_LydoId");
            CreateIndex("dbo.Thuchis", "NguoiThuchi_NguoiThuchiId");
            CreateIndex("dbo.Thuchis", "Lydo_LydoId");
            AddForeignKey("dbo.Thuchis", "NguoiThuchi_NguoiThuchiId", "dbo.Nguoithuchis", "NguoiThuchiId");
            AddForeignKey("dbo.Thuchis", "Lydo_LydoId", "dbo.Lydoes", "LydoId");
        }
    }
}
