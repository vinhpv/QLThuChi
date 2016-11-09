namespace QLThuChi.API.Migrations
{
    using Entities;
    using Microsoft.AspNet.Identity;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Validation;
    using System.Linq;
    using System.Text;

    internal sealed class Configuration : DbMigrationsConfiguration<QLThuChi.API.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(QLThuChi.API.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            //context.Users.AddOrUpdate(
            //    p => p.UserName,new Entities.ApplicationUser {UserName="sysadmin", EmailConfirmed=true, Level=1 }
            //    );

            //add Roles
            context.Roles.AddOrUpdate(p=>p.Name, new Microsoft.AspNet.Identity.EntityFramework.IdentityRole() { Name = "Admin" });
            context.Roles.AddOrUpdate(p => p.Name, new Microsoft.AspNet.Identity.EntityFramework.IdentityRole() { Name = "SysAdmin" });
            context.Roles.AddOrUpdate(p => p.Name, new Microsoft.AspNet.Identity.EntityFramework.IdentityRole() { Name = "User" });

            //add Lý do
            context.Lydoes.AddOrUpdate(p => p.TenLydo, new Entities.Lydo() { TenLydo = "Lĩnh lương tháng", KieuThu = Entities.Kieuthu.Thu });
            context.Lydoes.AddOrUpdate(p => p.TenLydo,new Entities.Lydo() { TenLydo = "Đi chợ", KieuThu = Entities.Kieuthu.Chi });
            context.Lydoes.AddOrUpdate(p => p.TenLydo,new Entities.Lydo() { TenLydo = "Trả nợ", KieuThu = Entities.Kieuthu.Chi });
            context.Lydoes.AddOrUpdate(p => p.TenLydo, new Entities.Lydo() { TenLydo = "Đóng tiền điện, nước", KieuThu = Kieuthu.Chi });

            //add Người thi chi
            context.Nguoithuchis.AddOrUpdate(p => p.HoTen, new Entities.Nguoithuchi() { HoTen = "Phạm Văn Vinh" });

            // add user
            var passwordHash = new PasswordHasher();
            string password = passwordHash.HashPassword("Tinhyeu#318");
            context.Users.AddOrUpdate(u => u.UserName,
                new ApplicationUser
                {
                    UserName = "sysadmin",
                    PasswordHash = password,
                    Level = 1,
                    EmailConfirmed = true,
                    Email = "vinhpv@outlook.com",
                    FirstName = "Vinh",
                    LastName = "Phạm Văn",
                    JoinDate = DateTime.Now.Date
                });
            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                StringBuilder sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new DbEntityValidationException(
                    "Entity Validation Failed - errors follow:\n" +
                    sb.ToString(), ex
                ); // Add the original exception as the innerException
            }
            catch (Exception ex)
            {
                throw ex;
            }

            


        }
    }
}
