using Microsoft.AspNetCore.Mvc;
using WebApplication2.Core.Model;

namespace WebApplication2.Core.Interfaces
{
    public interface ISemesterService
    {
        Task<Semester?> GetSemester(int id);
        Task<IEnumerable<Semester>> GetAllSemesters();
        Task<Semester> AddSemester(Semester sem);
        Task<Semester> EditSemester(int id, Semester sem);
        Task<bool> DeleteSemester(int id);
    }
}
