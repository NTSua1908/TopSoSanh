using Hangfire;
using Hangfire.MySql;
using MailKit;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using TopSoSanh.Entity;
using TopSoSanh.Services.Implement;
using TopSoSanh.Services.Interface;
using static TopSoSanh.Helper.Appsettings;

namespace TopSoSanh.Extentions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddCustomDbContext(this IServiceCollection services, WebApplicationBuilder builder)
        {
            services.AddEntityFrameworkMySql().AddDbContext<ApiDbContext>(options =>
                options.UseMySql(
                    builder.Configuration.GetConnectionString("ApiDbConnection"),
                    ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("ApiDbConnection"))
            ));

            builder.Configuration.Bind("MailSettings", new MailSettings());

            return services;
        }

        public static IServiceCollection RegisterApiServices(this IServiceCollection services)
        {
            services.AddScoped<ICrawlDataPhongVuService, CrawlDataPhongVuService>();
            services.AddScoped<ICrawlDataGearvnService, CrawlDataGearvnService>();
            services.AddScoped<ICrawlDataAnphatService, CrawlDataAnphatService>();
            services.AddScoped<ICrawlDataZShopService, CrawlDataZShopService>();
            services.AddScoped<ICrawlDataAnkhangService, CrawlDataAnkhangService>();
            services.AddScoped<ISendMailService, SendMailService>();
            services.AddScoped<ICrawlDataCommon, CrawlDataCommon>();
            services.AddScoped<IProductTrackingService, ProductTrackingService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            return services;
        }

        public static IServiceCollection AddHangfire(this IServiceCollection services, WebApplicationBuilder builder)
        {
            services.AddHangfire(x => x
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
            .UseSimpleAssemblyNameTypeSerializer()
            .UseRecommendedSerializerSettings()
            .UseStorage(
                new MySqlStorage(
                    builder.Configuration.GetConnectionString("ApiDbConnection"),
                    new MySqlStorageOptions
                    {
                        QueuePollInterval = TimeSpan.FromSeconds(30),
                        JobExpirationCheckInterval = TimeSpan.FromHours(1),
                        CountersAggregateInterval = TimeSpan.FromMinutes(5),
                        PrepareSchemaIfNecessary = true,
                        DashboardJobListLimit = 25000,
                        TransactionTimeout = TimeSpan.FromMinutes(5),
                        TablesPrefix = "Hangfire",
                    } 
                )
            ));

            // Add the processing server as IHostedService
            services.AddHangfireServer(options => options.WorkerCount = 1);

            return services;
        }
    }
}
