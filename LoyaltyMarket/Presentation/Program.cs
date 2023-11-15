using Domain.Services;
using Infrastructure.Configuration;
using Infrastructure.Data;
using Infrastructure.Data.Interfaces;

var builder = WebApplication.CreateBuilder(args);


// Configure CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAny", builder =>
        builder.AllowAnyOrigin()
               .AllowAnyHeader()
               .AllowAnyMethod());
});

// Add services to the container.
builder.Services.AddControllers();

//DB connection settings
builder.Services.Configure<LoyaltyMarketDatabaseSettings>(builder.Configuration.GetSection("LoyaltyMarketDatabase"));

//Service dependancies
builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddTransient<IProductService, ProductService>();

//Infrastructure dependancies
builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<IMongoConfiguration, MongoConfiguration>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

app.UseCors("AllowAny");

//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
