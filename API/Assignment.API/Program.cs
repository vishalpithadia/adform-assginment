using Assignment.Models.CurrencyRate;
using Microsoft.EntityFrameworkCore;
using Assignment.Job;
using Assignment.Repository.Interfaces;
using Assignment.Repository.Implementation;
using Assignment.Services.Interfaces;
using Assignment.Services.Implementation;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// Enable XML comments in Swagger
var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
builder.Services.AddSwaggerGen(options =>
{
    options.IncludeXmlComments(xmlPath);
    options.SupportNonNullableReferenceTypes();
});
builder.Services.AddDbContext<CurrencyRateDbContext>();

builder.Services.AddScoped<ICurrencyRateRepository, CurrencyRateRepository>();
builder.Services.AddScoped<ISyncLatestRatesService, SyncLatestRatesService>();
builder.Services.AddScoped<ICurrencyRatesService, CurrencyRatesService>();
builder.Services.AddScoped<ICalculatedHistoryRepository, CalculatedHistoryRepository>();
builder.Services.AddScoped<ICalculatedHistoryService, CalculatedHistoryService>();

builder.Services.AddHostedService<SyncLatestRatesServiceJob>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<CurrencyRateDbContext>();
    dbContext.Database.Migrate();
}


app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
