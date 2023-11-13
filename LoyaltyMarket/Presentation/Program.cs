using Domain.Services;
using Infrastructure.Configuration;
using Infrastructure.Data;
using Infrastructure.Data.Interfaces;
using MongoDB.Bson;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

//DB connection settings
builder.Services.Configure<LoyaltyMarketDatabaseSettings>(builder.Configuration.GetSection("LoyaltyMarketDatabase"));

//product dependancies
//builder.Services.AddSingleton<ProductService>();
//builder.Services.AddTransient<IProductService, ProductService>();

//Service dependancies
builder.Services.AddTransient<ICategoryService, CategoryService>();

//Infrastructure dependancies
builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();
builder.Services.AddTransient<IMongoConfiguration, MongoConfiguration>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(policy => policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
app.UseAuthorization();

app.MapControllers();

app.Run();
