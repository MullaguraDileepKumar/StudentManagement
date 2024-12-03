using WebApplication2.Core.Interfaces;
using WebApplication2.Core.Services;

namespace WebApplication2.Core.Config
{
    public static class ServiceInjection
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
           services.AddScoped<ICollegeService, CollegeService>();
            services.AddScoped<ITeacherService, TeacherService>();
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<ISubjectService, SubjectService>();
            services.AddScoped<IMarksService, MarksService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IDepartmentService, DepartmentService>();
            services.AddScoped<IAttendanceService, AttendanceService>();
            services.AddScoped<ISemesterService, SemesterService>();
            services.AddScoped<IStudentSubjectService, StudentSubjectService>();

            return services;
        }
        
    }
}
