using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Core.Data;
using WebApplication2.Core.Interfaces;
using WebApplication2.Core.Model;

namespace WebApplication2.Core.Services
{
    public class AttendanceService(ApplicationDbContext _context) : IAttendanceService
    {
        public async Task<Attendance?> GetAttendance(int id)
        {
            var attendance = await _context.Attendances.FirstOrDefaultAsync(x => x.Id == id);
                return attendance ?? null;
        }

        public IEnumerable<Attendance> GetAllAttendances()
        {
            var attendanceList = _context.Attendances.ToList();
            return attendanceList;
        }
        public async Task<Attendance> AddAttendance(Attendance attend)
        {
            try
            {
                await _context.AddAsync(attend);
                await _context.SaveChangesAsync();
                return attend;
            }
            catch (Exception ex) { throw ex; }
        }
        public async Task<Attendance> EditAttendance(int personId, [FromBody] Attendance attend)
        {
            try
            {
                var editedAttendance = await _context.Attendances.FirstOrDefaultAsync(a => a.PersonId == personId && a.RoleId == attend.RoleId);
                if (editedAttendance != null)
                {
                    editedAttendance.AttendancePercentage = attend.AttendancePercentage;
                     _context.Update(editedAttendance);
                    await _context.SaveChangesAsync();
                }
                return editedAttendance;
            }
            catch(Exception ex) { throw ex; }
        }
        public async Task<bool> DeleteAttendance(int personId, string roleId)
        {
            bool result = false;
            var record = await _context.Attendances.FirstOrDefaultAsync(a => a.PersonId == personId && a.RoleId == roleId);
            if(record != null)
            {
                _context.Remove(record);
                await _context.SaveChangesAsync();
                result = true;
            }
            return result;
        }
    }
}
