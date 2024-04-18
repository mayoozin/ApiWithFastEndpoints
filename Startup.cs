using ApiWithFastEndpoints.Model;
using FastEndpoints;
using Microsoft.AspNetCore.Hosting;

namespace ApiWithFastEndpoints
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<JWTModel>(Configuration.GetSection("JWTModel"));
        }
        //public void Configure(IApplicationBuilder app, IWebHostEnvironment)
        //{
        //  var user=  Configuration.GetSection("HangfireSettings:UserName").Value,
        // Configuration.GetSection("HangfireSettings:Password").Value
        //}
    }
}
