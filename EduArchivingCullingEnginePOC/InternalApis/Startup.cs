using Abstractions.Internal.Framework;
using Abstractions.Internal.Framework.Interfaces;
using Business.Framework;
using Business.SchoolDomains.CompulsorySchool;
using Business.SchoolDomains.KAA;
using DAL;
using InternalApis.BackgroundServices;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace InternalApis
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
            services.AddHttpClient();
            services.AddControllers();

            //TODO: Encapsulate all framework services in the extension method.
            //TODO: Try to register all new interfaces and their implemntation automatically
            services.AddSingleton<ICriterionDataAccess, CriterionDataAccess>();
            services.AddSingleton<ArchiveHandlerFactory>();
            services.AddSingleton<InputOutputFilesManager>();

            services.AddTransient<ISelectionDefinitionDataAccess, SelectionDefinitionDataAccess>();
            services.AddTransient<ISelectionDefinitionService, SelectionDefinitionBusiness>();
            //services.AddTransient<ISelectionExecutionHandler, SelectionExecutionHandlerWithEvents>();
            services.AddTransient<ISelectionExecutionHandler, SelectionExecutionHandler>();

            services.AddTransient<ArchiveFileCreationHandler>();
            services.AddTransient<CleanupHandler>();
            services.AddTransient<IDataReader, ExternalDataReader>();

            services.AddTransient<CompulsorySchoolArchiveHandler>();
            services.AddTransient<KAAArchiveHandler>();

            //services.AddTransient<ISelectionExecutionHandler, SelectionExecutionHandlerForBackgroundService>();
            //services.AddHostedService<ArchiveFileCreatorService>();
            //services.AddHostedService<CleanupService>();

            //services.AddSingleton<IArchive>(sp =>
            //{
            //    var indexPrefix = sp.GetService<IArchive>().;
            //    var dateTimeProvider = sp.GetService<IDateTimeProvider>();

            //    return new WeekBasedElasticIndexNameProvider(indexPrefix, dateTimeProvider);
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
