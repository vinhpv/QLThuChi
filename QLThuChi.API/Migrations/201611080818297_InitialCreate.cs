namespace QLThuChi.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Lydoes",
                c => new
                    {
                        LydoId = c.Int(nullable: false, identity: true),
                        TenLydo = c.String(),
                        Ghichu = c.String(),
                    })
                .PrimaryKey(t => t.LydoId);
            
            CreateTable(
                "dbo.Thuchis",
                c => new
                    {
                        ThuchiId = c.Int(nullable: false, identity: true),
                        NgayThuchi = c.DateTime(),
                        KieuThu = c.Boolean(),
                        Tien = c.Double(),
                        GhiChu = c.String(),
                        UserId = c.String(maxLength: 128),
                        Lydo_LydoId = c.Int(),
                        NguoiThuchi_NguoiThuchiId = c.Int(),
                    })
                .PrimaryKey(t => t.ThuchiId)
                .ForeignKey("dbo.Lydoes", t => t.Lydo_LydoId)
                .ForeignKey("dbo.Nguoithuchis", t => t.NguoiThuchi_NguoiThuchiId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.Lydo_LydoId)
                .Index(t => t.NguoiThuchi_NguoiThuchiId);
            
            CreateTable(
                "dbo.Nguoithuchis",
                c => new
                    {
                        NguoiThuchiId = c.Int(nullable: false, identity: true),
                        HoTen = c.String(),
                        GhiChu = c.String(),
                    })
                .PrimaryKey(t => t.NguoiThuchiId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FirstName = c.String(nullable: false, maxLength: 100),
                        LastName = c.String(nullable: false, maxLength: 100),
                        Level = c.Byte(nullable: false),
                        JoinDate = c.DateTime(nullable: false),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.UserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.TodoItems",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Task = c.String(),
                        Completed = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.Thuchis", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserLogins", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserClaims", "UserId", "dbo.Users");
            DropForeignKey("dbo.Thuchis", "NguoiThuchi_NguoiThuchiId", "dbo.Nguoithuchis");
            DropForeignKey("dbo.Thuchis", "Lydo_LydoId", "dbo.Lydoes");
            DropIndex("dbo.Roles", "RoleNameIndex");
            DropIndex("dbo.UserRoles", new[] { "RoleId" });
            DropIndex("dbo.UserRoles", new[] { "UserId" });
            DropIndex("dbo.UserLogins", new[] { "UserId" });
            DropIndex("dbo.UserClaims", new[] { "UserId" });
            DropIndex("dbo.Users", "UserNameIndex");
            DropIndex("dbo.Thuchis", new[] { "NguoiThuchi_NguoiThuchiId" });
            DropIndex("dbo.Thuchis", new[] { "Lydo_LydoId" });
            DropIndex("dbo.Thuchis", new[] { "UserId" });
            DropTable("dbo.TodoItems");
            DropTable("dbo.Roles");
            DropTable("dbo.UserRoles");
            DropTable("dbo.UserLogins");
            DropTable("dbo.UserClaims");
            DropTable("dbo.Users");
            DropTable("dbo.Nguoithuchis");
            DropTable("dbo.Thuchis");
            DropTable("dbo.Lydoes");
        }
    }
}
