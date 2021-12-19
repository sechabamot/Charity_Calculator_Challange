using Charity_Calculator_Challange.Data;
using Charity_Calculator_Challange.Interfaces;
using Charity_Calculator_Challange.Models;
using Charity_Calculator_Challange.Services;
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
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Charity_Calculator_Challange
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IConfiguration _configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddDbContext<ApplicationDbContext>(options =>
                     options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));

            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

            byte[] applicationSecret = System.Text.Encoding.ASCII.GetBytes(_configuration["Application:Secret"]);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(applicationSecret),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICalculateDeductiblesService, CalculateDeductiblesService>();

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors(options =>
                options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
            );

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseSpaStaticFiles();
            app.UseAuthentication();

            AddIdentityRoles(app).Wait();
            CreateInitialUsers(app).Wait();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";
                //spa.UseProxyToSpaDevelopmentServer("https://localhost:4200");

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                    spa.Options.StartupTimeout = TimeSpan.FromSeconds(180);

                }
            });
        }

        private async Task AddIdentityRoles(IApplicationBuilder app)
        {
            IServiceScopeFactory serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            using (IServiceScope scope = serviceScope.CreateScope())
            {
                RoleManager<IdentityRole> rolesManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                IEnumerable<string> roles = new List<string>(){ "Admin", "Donor", "Promoter"};

                foreach (string userRole in roles)
                {                   
                    if (!await rolesManager.RoleExistsAsync(userRole))
                    {
                        await rolesManager.CreateAsync(new IdentityRole(userRole));
                    }
                }
            }
        }

        private async Task CreateInitialUsers(IApplicationBuilder app)
        {
            var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>();
            using (IServiceScope scope = serviceScope.CreateScope())
            {
                var rolesManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var userManager = scope.ServiceProvider.GetService<UserManager<ApplicationUser>>();

                ApplicationUser admin = new ApplicationUser("admin@gmail.com");
                ApplicationUser donor = new ApplicationUser("donor@gmail.com");
                ApplicationUser promoter = new ApplicationUser("promoter@gmail.com");

                await userManager.CreateAsync(admin, "@Password4Now");
                await userManager.CreateAsync(donor, "@Password4Now");
                await userManager.CreateAsync(promoter, "@Password4Now");

                await userManager.AddToRoleAsync(admin, "Admin");
                await userManager.AddToRoleAsync(donor, "Donor");
                await userManager.AddToRoleAsync(promoter, "Promoter");

            }
        }


    }
}
