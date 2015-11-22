using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Http;
using Microsoft.Framework.DependencyInjection;
using Traveler.Services;
using Microsoft.AspNet.Hosting;
using Microsoft.Framework.Configuration;
using Microsoft.Framework.Runtime;

namespace Traveler
{
    public class Startup
    {

        public static IConfiguration Configuration;

        public Startup(IApplicationEnvironment appEnv) {

            var builder = new ConfigurationBuilder(appEnv.ApplicationBasePath)
                .AddJsonFile("config.json")
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services, IHostingEnvironment env)
        {
            services.AddMvc();

            // Setting up dependancy Injection
            if (env.IsDevelopment()) {
                services.AddScoped<IMailService, DebugMailService>();
            } else {
                //services.AddScoped<IMailService, RealMailService>();
                services.AddScoped<IMailService, DebugMailService>();
            }
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
