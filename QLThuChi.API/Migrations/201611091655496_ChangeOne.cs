namespace QLThuChi.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeOne : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Thuchis", "Lydo_LydoId", "dbo.Lydoes");
            DropForeignKey("dbo.Thuchis", "NguoiThuchi_NguoiThuchiId", "dbo.Nguoithuchis");
            DropIndex("dbo.Thuchis", new[] { "Lydo_LydoId" });
            DropIndex("dbo.Thuchis", new[] { "NguoiThuchi_NguoiThuchiId" });
            AlterColumn("dbo.Thuchis", "Lydo_LydoId", c => c.Int());
            AlterColumn("dbo.Thuchis", "NguoiThuchi_NguoiThuchiId", c => c.Int());
            CreateIndex("dbo.Thuchis", "Lydo_LydoId");
            CreateIndex("dbo.Thuchis", "NguoiThuchi_NguoiThuchiId");
            AddForeignKey("dbo.Thuchis", "Lydo_LydoId", "dbo.Lydoes", "LydoId");
            AddForeignKey("dbo.Thuchis", "NguoiThuchi_NguoiThuchiId", "dbo.Nguoithuchis", "NguoiThuchiId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Thuchis", "NguoiThuchi_NguoiThuchiId", "dbo.Nguoithuchis");
            DropForeignKey("dbo.Thuchis", "Lydo_LydoId", "dbo.Lydoes");
            DropIndex("dbo.Thuchis", new[] { "NguoiThuchi_NguoiThuchiId" });
            DropIndex("dbo.Thuchis", new[] { "Lydo_LydoId" });
            AlterColumn("dbo.Thuchis", "NguoiThuchi_NguoiThuchiId", c => c.Int(nullable: false));
            AlterColumn("dbo.Thuchis", "Lydo_LydoId", c => c.Int(nullable: false));
            CreateIndex("dbo.Thuchis", "NguoiThuchi_NguoiThuchiId");
            CreateIndex("dbo.Thuchis", "Lydo_LydoId");
            AddForeignKey("dbo.Thuchis", "NguoiThuchi_NguoiThuchiId", "dbo.Nguoithuchis", "NguoiThuchiId", cascadeDelete: true);
            AddForeignKey("dbo.Thuchis", "Lydo_LydoId", "dbo.Lydoes", "LydoId", cascadeDelete: true);
        }
    }
}
