using TopSoSanh.Entity;
using Microsoft.EntityFrameworkCore;
using static TopSoSanh.Helper.Appsettings;
using TopSoSanh.Extentions;
using Hangfire;
using System.Net;

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

app.UseHangfireDashboard();

app.UseAuthorization();

app.UseCors();

//app.UseEndpoints(endpoints =>
//    endpoints.MapHangfireDashboard("/hangfire", new DashboardOptions
//    {
//        IgnoreAntiforgeryToken = true
//    })
//);

app.MapControllers();

app.Run();
