using TopSoSanh.Services.Implement;
using TopSoSanh.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.WithOrigins("http://localhost:3000", "null")
                                .AllowAnyHeader()
                                .AllowAnyMethod();
        });
});

builder.Services.AddScoped<ICrawlDataPhongVuService, CrawlDataPhongVuService>();
builder.Services.AddScoped<ICrawlDataGearvnService, CrawlDataGearvnService>();
builder.Services.AddScoped<ICrawlDataAnphat, CrawlDataAnphat>();
builder.Services.AddScoped<ICrawlDataZShopService, CrawlDataZShopService>();
builder.Services.AddScoped<ICrawlDataCommon, CrawlDataCommon>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()|| app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors();

app.MapControllers();

app.Run();
