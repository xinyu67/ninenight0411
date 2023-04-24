using DI.Service;
using Microsoft.Extensions.FileProviders;
using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// 讀取設定檔
var configurationBuilder = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
var configuration = configurationBuilder.Build();

builder.Services.AddControllers();
builder.Services.AddControllersWithViews();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

// goose
builder.Services.AddSingleton<IFileProvider>
(
    new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"))
);

//連接appsrttings的local連線字串
var connectionString = configuration.GetConnectionString("local");
//建立服務時如需其他相依服務
builder.Services.AddSingleton<SqlConnection>(_ => new SqlConnection(connectionString));
builder.Services.AddSingleton<ProductDBService>();
builder.Services.AddSingleton<StoryDBService>();
builder.Services.AddSingleton<BrandDBService>();
builder.Services.AddSingleton<PlaceDBService>();
builder.Services.AddSingleton<NewDBService>();
builder.Services.AddSingleton<StoreDBService>();
builder.Services.AddSingleton<CartDBService>();
builder.Services.AddSingleton<IndexDBService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// goose
app.UseStaticFiles
(
    new StaticFileOptions()
    {
        FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot", "image")),
        RequestPath = new PathString("/image")
    }
);

app.UseHttpsRedirection();

// goose
app.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

// app.UseCors(builder => builder.WithOrigins("http://127.0.0.1:5555").AllowAnyHeader().AllowAnyMethod());

app.UseAuthorization();

app.MapControllers();

app.Run();