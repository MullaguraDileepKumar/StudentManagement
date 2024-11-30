using Microsoft.AspNetCore.Mvc;
using WebApplication2.Core.Interfaces;
using WebApplication2.Core.Model;


namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController(IAttendanceService _attendanceService) : ControllerBase
    {

        [HttpGet("GetAttendance/{id}")]
        public async Task<ActionResult<Attendance>> GetAttendance(int id)
        {
            var attendance = await _attendanceService.GetAttendance(id);
            return Ok(attendance);
        }

        [HttpGet("GetAllAttendances")]
        public IEnumerable<Attendance> GetAllAttendances()
        {
            var attendanceList = _attendanceService.GetAllAttendances();
            return attendanceList;
        }

        [HttpPost("AddAttendance{id}")]
        public async Task<ActionResult<Attendance>> AddAttendance([FromBody] Attendance attend)
        {
            var newAttendance = await _attendanceService.AddAttendance(attend);
            return Ok(newAttendance);
        }

        [HttpPut("EditAttendance{personId}")]
        public async Task<ActionResult<Attendance>> EditAttendance(int personId, [FromBody] Attendance attend)
        {
            var response = await _attendanceService.EditAttendance(personId, attend);
            return response != null ? Ok(response) :NotFound();
        }
       
        [HttpDelete("DeleteAttendance")]
        public async Task<ActionResult<bool>> DeleteAttendance(int personId, string roleId)
        {
            var isDeleted = await _attendanceService.DeleteAttendance(personId, roleId);
            return isDeleted ? Ok() : NotFound();
        }
    }
}
