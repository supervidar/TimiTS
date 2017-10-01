using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using TimiTS.Models;
using TimiTS.Models.IRepository;
using TimiTS.Models.EFRepository;

namespace TimiTS
{
    public class Startup
    {
        IConfigurationRoot Configuration;

        public Startup(IHostingEnvironment env)
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json").Build();
        }
        

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
            services.AddSingleton<IProjectRepository, EFProjectRepository>();
            services.AddSingleton<IContractorRepository, EFContractorRepository>();
            services.AddSingleton<IWorkParticipationRepository, EFWorkParticipationRepository>();
            services.AddSingleton<IFeedbackRepository, EFFeedbackRepository>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {

            app.UseIdentity();


            // Displayes details of exceptions that occur in the application, which is useful during development
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            

            // Adds a simple message HTTP responses that would not otherwise have a body, such as 404 - not Found responses.
            app.UseStatusCodePages();

            // enables support for serving static content from the wwwroot folder
            app.UseStaticFiles();

            //Tells MVC that it should send requests that arrive for the root URL of the application
            //to the Index action method in the HomeController class. 
            app.UseMvc(routes => {

                routes.MapRoute(
                    name: "areas",
                    template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });


        }
    }
}
