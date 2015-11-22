using Microsoft.AspNet.Builder;
using Microsoft.Framework.DependencyInjection;
using Traveler.Services;
using Microsoft.AspNet.Hosting;
using Microsoft.Framework.Configuration;
using Microsoft.Dnx.Runtime;
using Traveler.Models;

namespace Traveler
{
    public class Startup
    {

        public static IConfigurationRoot Configuration;

        public Startup(IApplicationEnvironment appEnv) {

            var builder = new ConfigurationBuilder(appEnv.ApplicationBasePath)
                .AddJsonFile("config.json")
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Add Mvc
            services.AddMvc();

            // Add Entity Frameworl
            services.AddEntityFramework()
                .AddSqlServer()
                .AddDbContext<TravelerContext>();

            // Setting up dependancy Injection
            services.AddScoped<IMailService, DebugMailService>();
        }

        public void Configure(IApplicationBuilder app)
        {
            // Enable serving of static file
            app.UseStaticFiles();
            // Enable MVC app
            app.UseMvc(config => {
                config.MapRoute(
                    name: "Default",
                    template: "{controller}/{action}/{id?}",
                    defaults: new  { controller = "App", action = "Index" }
                );
            });
        }
    }
}
