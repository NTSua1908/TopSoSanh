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
            return services;
        }
    }
}
