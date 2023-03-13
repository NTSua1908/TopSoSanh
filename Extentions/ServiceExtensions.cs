using Hangfire;
using Hangfire.MySql;
using MailKit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using TopSoSanh.DTO;
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
            builder.Configuration.Bind("JWTToken", new JWTToken());

            return services;
        }

        public static IServiceCollection RegisterApiServices(this IServiceCollection services)
        {
            services.AddScoped<ICrawlDataPhongVuService, CrawlDataPhongVuService>();
            services.AddScoped<ICrawlDataGearvnService, CrawlDataGearvnService>();
            services.AddScoped<ICrawlDataAnphatService, CrawlDataAnphatService>();
            services.AddScoped<ICrawlDataZShopService, CrawlDataZShopService>();
            services.AddScoped<ICrawlDataAnkhangService, CrawlDataAnkhangService>();
            services.AddScoped<ICrawlDataCustomShopService, CrawlDataCustomShopService>();
            services.AddScoped<ICrawlDataCommon, CrawlDataCommon>();
            services.AddScoped<ISendMailService, SendMailService>();
            services.AddScoped<IProductTrackingService, ProductTrackingService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            return services;
        }

        public static IServiceCollection AddHangfire(this IServiceCollection services, WebApplicationBuilder builder)
        {
            services.AddHangfire(x => x
            .SetDataCompatibilityLevel(CompatibilityLevel.Version_110)
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

        public static IServiceCollection AddJWT(this IServiceCollection services, WebApplicationBuilder builder)
        {
            // ===== Add Jwt Authentication ========
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); // => remove default claims
            services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(cfg =>
                {
                    cfg.RequireHttpsMetadata = false;
                    cfg.SaveToken = true;
                    cfg.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidAudiences = new List<string>()
                        {
                            builder.Configuration.GetValue<string>("JWTToken:JwtAudienceId")
                        },
                        ValidIssuer = builder.Configuration.GetValue<string>("JWTToken:JwtIssuer"),
                        ValidAudience = builder.Configuration.GetValue<string>("JWTToken:JwtIssuer"),
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("JWTToken:JwtKey"))),
                        ClockSkew = TimeSpan.Zero // remove delay of token when expire,
                    };
                    cfg.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                            {
                                context.Response.Headers.Add("Token-Expired", "true");
                            }
                            return Task.CompletedTask;
                        }
                    };
                });//.AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));
            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Topsosanh APP API", Version = "v1.0.0" });
                c.AddSecurityDefinition("Bearer",
                    new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                    {
                        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                        Description = "Please enter into field the word 'Bearer' following by space and JWT",
                        Name = "Authorization",
                        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey
                    });
                c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement()
                    {
                    {
                        new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                        {
                            Reference = new Microsoft.OpenApi.Models.OpenApiReference
                            {
                                Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = Microsoft.OpenApi.Models.ParameterLocation.Header
                        },
                        new List<string>()
                    }
                    });
            });
            return services;
        }
    }
}
