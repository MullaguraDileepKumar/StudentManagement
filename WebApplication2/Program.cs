using Microsoft.EntityFrameworkCore;
using Serilog;
using System.Text.Json.Serialization;
using WebApplication2.Core.Config;
using WebApplication2.Core.Data;


var builder = WebApplication.CreateBuilder(args);


Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()  // Set the minimum log level to Information
    .WriteTo.File("Logs/log-.txt", rollingInterval: RollingInterval.Day)  // File logging with daily rolling
    .CreateLogger();


builder.Logging.ClearProviders();
// Use Serilog as the logging provider
builder.Host.UseSerilog();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {

        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());

    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
          options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddServices();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();


app.MapControllers();
app.Run();
