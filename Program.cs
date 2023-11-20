using CrmIntegration.Adapters.Altegio;
using CrmIntegration.Adapters.Kommo;
using CrmIntegration.Options;
using CrmIntegration.Services;
using NLog;
using NLog.Web;
using System;

namespace CrmIntegration
{
    internal class Program
    {
        private static void AddOptions(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<KommoIntegrationOptions>(options =>
                configuration.GetSection(KommoIntegrationOptions.SectionName).Bind(options));
            services.Configure<AltegioIntegrationOptions>(options =>
                configuration.GetSection(AltegioIntegrationOptions.SectionName).Bind(options));
        }

        private static void AddServices(IServiceCollection services)
        {
            services.AddScoped<IKommoAdapter, KommoAdapter>();
            services.AddScoped<IAltegioAdapter, AltegioAdapter>();
            services.AddScoped<IIntegrationService, IntegrationService>();
        }

        private static async Task Main(string[] args)
        {
            var logger = LogManager.Setup().GetCurrentClassLogger();
            logger.Info("Application started.");

            try
            {
                var builder = WebApplication.CreateBuilder(args);
                builder.Logging.ClearProviders();
                builder.Host.UseNLog();

                AddOptions(builder.Services, builder.Configuration);
                AddServices(builder.Services);
                builder.Services.AddControllers();
                builder.Services.AddRouting();

                var app = builder.Build();
                app.UseRouting();
                app.UseEndpoints(endpoints => endpoints.MapControllers());

                await app.RunAsync();
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Stopped program because of exception");
                throw;
            }
            finally
            {
                LogManager.Shutdown();
            }
        }
    }
}