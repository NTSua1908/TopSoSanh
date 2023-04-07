using TopSoSanh.Entity;
using Microsoft.EntityFrameworkCore;
using static TopSoSanh.Helper.Appsettings;
using TopSoSanh.Extentions;
using Hangfire;
using System.Net;
using Hangfire.Dashboard;
using TopSoSanh.Helper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCustomDbContext(builder);
builder.Services.AddHangfire(builder);
builder.Services.RegisterApiServices();

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
                    policy => policy.AllowAnyHeader()
                                    .AllowAnyMethod()
                                    .SetIsOriginAllowed(origin => true)
                                    .AllowCredentials());
});

builder.Services.AddJWT(builder);
builder.Services.AddSwagger();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<ApiDbContext>();
        context.Database.Migrate();
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()|| app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

var options = new DashboardOptions
{
    Authorization = new[]{
        new HangfireAuthorizationFilter()
    }
};

app.UseHangfireDashboard("/hangfire", options);

app.UseAuthorization();

app.UseCors();

//app.UseEndpoints(endpoints =>
//    endpoints.MapHangfireDashboard("/hangfire", new DashboardOptions
//    {
//        IgnoreAntiforgeryToken = true
//    })
//);

app.MapControllers();
//NetworkManagement.setIP("27.74.252.200", "255.255.240.0");
app.Run();
