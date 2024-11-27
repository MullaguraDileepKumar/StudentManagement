using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Text.Json.Serialization;
using WebApplication2.Core.Data;
using WebApplication2.Core.Interfaces;
using WebApplication2.Core.Services;
internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
               // options.JsonSerializerOptions.Converters.Add(new DateOnlyConverter());
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                
            });
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection")));

        builder.Services.AddScoped<ICollegeService, CollegeService>();
        builder.Services.AddScoped<ITeacherService, TeacherService>();
        builder.Services.AddScoped<IStudentService, StudentService>();
        builder.Services.AddScoped<ISubjectService, SubjectService>();
        builder.Services.AddScoped<IMarksService, MarksService>();
        builder.Services.AddScoped<IRoleService, RoleService>();
        builder.Services.AddScoped<IDepartmentService, DepartmentService>();
        builder.Services.AddScoped<IAttendanceService, AttendanceService>();



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
    }
}