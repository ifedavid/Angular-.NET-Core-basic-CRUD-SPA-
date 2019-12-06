using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;
using WebApplication5.Data;

namespace WebApplication5
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // In production, the Angular files will be served from this directory
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            //enable CORS
            services.AddCors(option =>
            {
                option.AddPolicy("EnableCors", builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod().AllowCredentials().Build();
                });

            });

            services.AddDbContext<ApplicationDbContext>(options =>
            
                options.UseSqlServer(Configuration.GetConnectionString("AngularTours")));

            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                //password settings
                options.Password.RequireDigit = true;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireUppercase = true;
                options.Password.RequireNonAlphanumeric = false;

                //lockut settings
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(45);
            }
                
                ).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("IsloggedIn", policy => policy.RequireRole("Admin", "Customer", "Planner").RequireAuthenticatedUser());
                options.AddPolicy("IsaPlanner", policy => policy.RequireRole("Admin", "Planner").RequireAuthenticatedUser());
                options.AddPolicy("IsAdmin", policy => policy.RequireRole("Admin").RequireAuthenticatedUser()); 
            }
            );

            services.Configure<IdentityOptions>(options =>
            {
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                options.Lockout.MaxFailedAccessAttempts = 5;
               
            });

            services.AddAuthentication(options =>
            {

                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidIssuer = "ifeoluwa",
                        ValidAudience = "ifeoluwa",
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MySuperSecureKey"))
                    };
                });


        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseCors("EnableCors");

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseFileServer();
            app.UseSpaStaticFiles();
            app.UseAuthentication();
          
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
                routes.MapRoute(
                   name: "config",
                   template: "{controller}/{action=Index}/{string?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";
                //Configure the timeout to 5 minutes to avoid "The Angular CLI process did not start listening for requests within the timeout period of 50 seconds." issue
                spa.Options.StartupTimeout = new TimeSpan(0, 5, 0);


                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }

            });


        }
    }
}
