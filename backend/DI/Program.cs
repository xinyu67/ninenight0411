using DI.Service;
using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Ū���]�w��
var configurationBuilder = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
var configuration=configurationBuilder.Build();

builder.Services.AddControllers();
builder.Services.AddControllersWithViews();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//�s��appsrttings��local�s�u�r��
var connectionString = configuration.GetConnectionString("local");
//�إߪA�Ȯɦp�ݨ�L�̪ۨA��
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

app.UseHttpsRedirection();

app.UseCors(builder => builder.WithOrigins("http://127.0.0.1:5555").AllowAnyHeader().AllowAnyMethod());

app.UseAuthorization();

app.MapControllers();

app.Run();