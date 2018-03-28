using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.Webpack;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Vega_New.Core;
using Vega_New.Core.Models;
using Vega_New.Persistence;

namespace Vega_New
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
            services.Configure<PhotoSettings>(Configuration.GetSection("PhotoSettings"));
            services.AddScoped<IVehicleRepository, VehicleRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddAutoMapper();

            //this is where we can register services for dependency injections such as
            //services.AddTransient<IRepository, EFRepository> -- this creates an instance of EF whenever the IRepository is in the constructor of a method (such as in a controller)

            //we are adding our context file here so that we can inject it into our controllers. the advantage of doing this over doing something like... 
            //VegaDbContext myContext = new VegaDbContext();
            //is because it allows for loose coupling. By using DI we can inject the context into a controller, and it will automatically create an instance of this context
            //we can also pass fake data for unit testing in the constructor which is the primary benefit
            services.AddDbContext<VegaDbContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:Default"]));

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //look into logger factory in console logs aren't showing up

            //this is where we customize our middleware -- everything below this is middleware

            //to change environment at system level go to ControlPanel > System > Advanced Settings -- create a new user varialble names ASPNETCORE_ENVIRONMENT wit the value set to Development
            //to change env at terminal level type "$Env:ASPNETCORE_ENVIRONMENT="Development""
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebpackDevMiddleware(new WebpackDevMiddlewareOptions
                {
                    //allows us to not have to manually rebuild everytime we change something in angular.
                    HotModuleReplacement = true
                });
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapSpaFallbackRoute(
                    name: "spa-fallback",
                    defaults: new { controller = "Home", action = "Index" });
            });
        }
    }
}
