using Logging.Abstractions;
using Logging.SerilogClient;
using Logging.SerilogClient.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LoggingNetCore
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
            services.AddControllers();
            services.AddHttpContextAccessor();

            services.AddMetaService();
            services.AddMetaServiceInHttpContext(configure =>
            {
                configure.Domain("TESSA_META");
                configure.MetaServiceUri("http://tecedupune.ap.tieto.com:23610");
            });
            services.AddDatabaseManagement();
            services.AddDatabaseManagementOnWindows();
            
           // services.Configure<SerilogSqlSinkOptions>(o => { o.ModuleName = "ArchivingCulling"; });
           // services.ConfigureOptions<SerilogPostConfigureOptions>();
            services.AddScoped(typeof(IAuditLogger<>), typeof(AuditLogger<>));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


        }
    }
}
