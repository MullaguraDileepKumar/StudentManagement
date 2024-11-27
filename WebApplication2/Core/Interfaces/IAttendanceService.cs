using Microsoft.AspNetCore.Mvc;
using WebApplication2.Core.Model;

namespace WebApplication2.Core.Interfaces
{
    public interface IAttendanceService
    {
        Task<Attendance?> GetAttendance(int id);
        IEnumerable<Attendance> GetAllAttendances();
        Task<Attendance> AddAttendance(Attendance attend);
        Task<Attendance> EditAttendance(int personId, [FromBody] Attendance attend);
        Task<bool> DeleteAttendance(int personId, string roleId);
    }
}
