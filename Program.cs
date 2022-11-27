using TopSoSanh.Services.Implement;
using TopSoSanh.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ICrawlDataPhongVuService, CrawlDataPhongVuService>();
builder.Services.AddScoped<ICrawlDataGearvnService, CrawlDataGearvnService>();
builder.Services.AddScoped<ICrawlDataTheGioDiDongService, CrawlDataTheGioDiDongService>();
builder.Services.AddScoped<ICrawlDataZShopService, CrawlDataZShopService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
