using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Scheduler.Data;
using Scheduler.Models;
using Scheduler.Services;
using Microsoft.AspNetCore.Mvc.Razor;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.OData.Extensions;
using Microsoft.AspNet.OData.Builder;
using Scheduler.Models.HealthCenter;
using Newtonsoft.Json.Serialization;
using Microsoft.Extensions.FileProviders;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging.Console;

namespace Scheduler
{
    public class Startup
    {

        public CultureInfo[] SupportedCultures { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;



        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(builder =>
            {
                builder.AddConfiguration(Configuration.GetSection("Logging"))
                    .AddConsole()
                    .AddDebug();
            });

            services.AddLocalization(options => options.ResourcesPath = "Resources");

            services.AddDbContext<ApplicationDbContext>(options =>
                options
                    .UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddAuthentication().AddFacebook(facebookOptions =>
            {
                facebookOptions.AppId = Configuration["Authentication:Facebook:AppId"];
                facebookOptions.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
            });

            services.AddAuthentication().AddGoogle(googleOptions =>
            {
                googleOptions.ClientId = Configuration["Authentication:Google:ClientId"];
                googleOptions.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
            });

            // Add application services.
            services.AddTransient<IEmailSender, EmailSender>();

            services.AddMvc()
                .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix, 
               opts => { opts.ResourcesPath = "Resources"; })
                .AddJsonOptions(options => options.SerializerSettings.ContractResolver = new DefaultContractResolver());

        }



        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider, ILoggerFactory loggerFactory)
        {
            // logging
            loggerFactory.AddFile("scheduler.log");
            var logger = loggerFactory.CreateLogger<ConsoleLogger>();
            logger.LogInformation("Executing Configure()");


            ////--Start
            //var builder = new ODataConventionModelBuilder();
            //builder.EntitySet<Doctor>(nameof(Doctor));
            ////--End
            var locOptions = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(locOptions.Value);

            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            var supportedCultures = new[]
            {
                new CultureInfo("en-US"),
                new CultureInfo("lt-LT")
            };

            var options = new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("en-US"),
                SupportedCultures = supportedCultures,
                SupportedUICultures = supportedCultures
            };

            app.UseRequestLocalization(options);

            //var requestLocalizationOptions = new RequestLocalizationOptions
            //{
            //    DefaultRequestCulture = new RequestCulture(supportedCultures[0]),
            //    // Formatting numbers, dates, etc.
            //    SupportedCultures = supportedCultures,
            //    // UI strings that we have localized.
            //    SupportedUICultures = supportedCultures,

            //};
            //requestLocalizationOptions.RequestCultureProviders.Add(new QueryStringRequestCultureProvider{ Options = requestLocalizationOptions });
            //requestLocalizationOptions.RequestCultureProviders.Add(new CookieRequestCultureProvider { Options = requestLocalizationOptions });

            //app.UseRequestLocalization(requestLocalizationOptions);

            app.UseStaticFiles();
 
            app.UseAuthentication();
            
            app.UseMvc(routes =>
            {
                //routes.MapODataRoute("odata", builder.GetEdmModel());
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            CreateRoles(serviceProvider).Wait();
            CreateAdminUser(serviceProvider).Wait();
        }

        private async Task CreateRoles(IServiceProvider serviceProvider)
        {
            //adding custom roles
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            
            string[] roleNames = { "Admin", "Doctor", "Patient" };
            IdentityResult roleResult;
            
            foreach (var roleName in roleNames)
            {
                //creating the roles and seeding them to the database
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
        }

        private async Task CreateAdminUser(IServiceProvider serviceProvider)
        {
            var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            //creating a super user who could maintain the web app
            var poweruser = new ApplicationUser
            {
                UserName = Configuration.GetSection("AdminUser")["UserEmail"],
                Email = Configuration.GetSection("AdminUser")["UserEmail"]
            };

            string UserPassword = Configuration.GetSection("AdminUser")["UserPassword"];
            var _user = await UserManager.FindByEmailAsync(Configuration.GetSection("AdminUser")["UserEmail"]);

            if (_user == null)
            {
                var createPowerUser = await UserManager.CreateAsync(poweruser, UserPassword);
                if (createPowerUser.Succeeded)
                {
                    //here we tie the new user to the "Admin" role 
                    await UserManager.AddToRoleAsync(poweruser, "Admin");
                }
            }
        }
    }
}
