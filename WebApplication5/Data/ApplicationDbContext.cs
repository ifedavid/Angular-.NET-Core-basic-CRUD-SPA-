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
                new { Id = "2", Name = "customer", NormalizedName = "CUSTOMER" },
                new { Id = "3", Name = "planner", NormalizedName = "PLANNER" }

                );
   
        }

        public DbSet<ToursModel> Tours { get; set; }
        public DbSet<TourPlannersModel> TourPlanners { get; set; }
    }
}
