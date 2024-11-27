using Microsoft.AspNetCore.Mvc;
using WebApplication2.Core.Model;

namespace WebApplication2.Core.Interfaces
{
    public interface ITeacherService
    {
        Task<IEnumerable<Teacher>> GetAllTeachers();
        Task<bool> AddTeacher(Teacher teacher);
        Task<Teacher?> EditTeacher(int id,Teacher teacher);
        Task<bool> DeleteTeacher(int id);
    }
}
