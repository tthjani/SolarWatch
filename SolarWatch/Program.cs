using Microsoft.EntityFrameworkCore;
using SolarWatch.Data;
using SolarWatch.Repository;
using SolarWatch.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddDbContext<SolarWatchContext>(options =>
    options.UseSqlServer("Server=localhost,1435;Database=focused_beaver;User Id=sa;Password=yourStrong(!)Password;Encrypt=false;"));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ISunriseSunsetDataProvider, SunriseSunsetApi>();
builder.Services.AddScoped<IJsonProcessor, JsonProcessor>();
builder.Services.AddScoped<ICityNameDataProvider, GeocodingApi>();
builder.Services.AddScoped<IGeoJsonProcessor, GeoJsonProcessor>();
builder.Services.AddScoped<ICityRepository, CityRepository>();
builder.Services.AddScoped<ISunriseSunsetRepository, SunriseSunsetRepository>();

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

app.UseDeveloperExceptionPage();

app.Run();