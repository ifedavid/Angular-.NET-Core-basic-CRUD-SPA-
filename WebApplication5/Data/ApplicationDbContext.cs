using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication5.Models;

namespace WebApplication5.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        //creating roles on building the model

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRole>().HasData(
                new { Id = "1", Name = "admin", NormalizedName = "ADMIN" },
                new { Id = "2", Name = "user", NormalizedName = "USER" }
                );

            builder.Entity<DailySpendings>()
                .HasIndex(ds => ds.Date)
                .IsUnique();
   
        }



        public DbSet<UserData> UserData { get; set; }
        public DbSet<DailySpendings> Spendings { get; set; }
        public DbSet<Category> Categories { get; set; }
       
    }
}
