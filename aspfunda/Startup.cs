using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using aspfunda.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace aspfunda
{
    public class Startup
    {
        private IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddDbContextPool<AppDbContext>(options => options.UseSqlServer(_config.GetConnectionString("EmployeeDbConnection")));
            services.AddIdentity<ApplicationUser, IdentityRole>(options => {
                options.Password.RequiredLength = 6;
                //options.Password.RequiredUniqueChars = 2;
               // options.Password.RequireNonAlphanumeric = false;
            }
            
            ).AddEntityFrameworkStores<AppDbContext>();
            

            
            services.AddMvc(config => {
                var policy = new AuthorizationPolicyBuilder().
                RequireAuthenticatedUser().Build();
                config.Filters.Add(new AuthorizeFilter(policy));
            });
            //  services.AddMvcCore();
            services.AddScoped <IEmployeeRepository, SQLEmployeerepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env/*, ILogger<Startup> logger*/)
        {
            if (env.IsDevelopment())
            {
                //DeveloperExceptionPageOptions developerExceptionPageOptions = new DeveloperExceptionPageOptions
                //{
                //    SourceCodeLineCount = 1
                //};
                app.UseDeveloperExceptionPage();
            }
            else {
                app.UseExceptionHandler("/Error");
                app.UseStatusCodePagesWithRedirects("/Error/{0}");
            }
            //app.Use(async (context , Next) =>
            //{
            //    //await context.Response.WriteAsync("Hello from 2 nd Middleware");
            //    logger.LogInformation("MW1 : Incomming Request");
            //    await Next();
            //    logger.LogInformation("MW1 : OutGoing Response");

            //});
            //app.Use(async (context, Next) =>
            //{
            //    //await context.Response.WriteAsync("Hello from 2 nd Middleware");
            //    logger.LogInformation("MW2 : Incomming Request");
            //    await Next();
            //    logger.LogInformation("MW2 : OutGoing Response");

            //});
           // DefaultFilesOptions defaultFilesOptions = new DefaultFilesOptions();
            //defaultFilesOptions.DefaultFileNames.Clear();
            //defaultFilesOptions.DefaultFileNames.Add("eviiii.html");

            // app.UseDefaultFiles(defaultFilesOptions);
            app.UseStaticFiles();
            app.UseAuthentication();
            // app.UseFileServer();
            // app.UseMvcWithDefaultRoute();
           // app.UseMvc();
            app.UseMvc(route => { route.MapRoute("default", "{controller=Home}/{action=Index}/{id?}"); });

            //app.Run(async (context) =>
            //{
            //    // throw new Exception("Some Error Processing Request occur");

           // await context.Response.WriteAsync(System.Diagnostics.Process.GetCurrentProcess().ProcessName);
            //    //logger.LogInformation("MW3 : Request Handle and Response Produce");
            //});
        }
    }
}
