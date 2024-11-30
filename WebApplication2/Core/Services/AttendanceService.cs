using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Core.Data;
using WebApplication2.Core.Interfaces;
using WebApplication2.Core.Model;

namespace WebApplication2.Core.Services
{
    public class AttendanceService(ApplicationDbContext _context,ILogger<AttendanceService> _logger) : IAttendanceService
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
                var attendanceList = await _context.Attendances.Where(a => a.PersonId == attend.PersonId).ToListAsync();
               if (attendanceList.Count > 0)
                {
                    foreach (var attendance in attendanceList)
                    {
                        if (attendance.RoleId == attend.RoleId)
                        {
                            _context.Attendances.Update(attend);
                            await _context.SaveChangesAsync();
                            _logger.LogInformation("Updated the Person attendance from add attendance",attend.PersonId ,"RoleId: ",attend.RoleId);
                        }
                    }
                    return attend;
                }
               else
                {
                    await _context.AddAsync(attend);
                    await _context.SaveChangesAsync();
                    _logger.LogInformation("New attendance added :", attend.PersonId, "RoleId: ", attend.RoleId);
                    return attend;
                }
                
            }
            catch (Exception ex)
            {
                _logger.LogError(ex,"Error during add attendance to Person: ",attend.PersonId ,"Role :",attend.RoleId);
                throw;
            }
        }
        public async Task<Attendance> EditAttendance(int personId, [FromBody] Attendance attend)
        {
            try
            {
                var editedAttendance = await _context.Attendances.FirstOrDefaultAsync(a => a.PersonId == personId && a.RoleId == attend.RoleId);
                if (editedAttendance != null)
                {
                    editedAttendance.AttendancePercentage = attend.AttendancePercentage;
                     _context.Attendances.Update(editedAttendance);
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
                _context.Attendances.Remove(record);
                await _context.SaveChangesAsync();
                result = true;
            }
            return result;
        }
    }
}
