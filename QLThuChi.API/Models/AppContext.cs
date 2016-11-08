using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;
using QLThuChi.API.Entities;
using System.Data.Entity;

namespace QLThuChi.API.Models
{

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
    : base("DefaultConnection", throwIfV1Schema: false)
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = true;
            Database.SetInitializer(new
            DropCreateDatabaseIfModelChanges<ApplicationDbContext>());
        }

        public DbSet<TodoItem> todos { get; set; }
        public DbSet<Lydo> Lydoes { get; set; }
        public DbSet<Nguoithuchi> Nguoithuchis { get; set; }
        public DbSet<Thuchi> Thuchis { get; set; }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ApplicationUser>().ToTable("Users");
            modelBuilder.Entity<IdentityRole>().ToTable("Roles");
            modelBuilder.Entity<IdentityUserRole>().ToTable("UserRoles");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("UserLogins");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("UserClaims");

            modelBuilder.Entity<Thuchi>()
                    .HasRequired(s => s.NguoiThuchi)
                    .WithMany(s => s.Thuchis);

            modelBuilder.Entity<Thuchi>()
                   .HasRequired(s => s.Lydo)
                   .WithMany(s => s.Thuchis);

            modelBuilder.Entity<Thuchi>()
                  .HasRequired<ApplicationUser>(s => s.User)
                  .WithMany(s => s.Thuchis);

        }
    }
}