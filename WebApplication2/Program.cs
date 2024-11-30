using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Extensions.Logging;
using System.Text.Json.Serialization;
using WebApplication2.Core.Config;
using WebApplication2.Core.Data;
internal class Program
{
    private static void Main(string[] args)
    {

       
        var builder = WebApplication.CreateBuilder(args);
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
    }
}