using Presentation.Models;
using Presentation.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

//DB connection settings
builder.Services.Configure<LoyaltyMarketDatabaseSettings>(builder.Configuration.GetSection("LoyaltyMarketDatabase"));

//product dependancies
//builder.Services.AddSingleton<ProductService>();
builder.Services.AddTransient<IProductService, ProductService>();

//category dependancies
//builder.Services.AddSingleton<CategoryService>();
builder.Services.AddTransient<ICategoryService, CategoryService>();


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

app.UseAuthorization();

app.MapControllers();

app.Run();
