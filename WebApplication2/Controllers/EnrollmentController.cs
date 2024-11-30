using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Core.Constants;
using WebApplication2.Core.Data;
using WebApplication2.Core.Model;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnrollmentController(ApplicationDbContext _context) : ControllerBase
    {
       
        [HttpGet("GetEnrollByStudent/{studentId}")]
        public async Task<ActionResult<Enrollment>> GetEnrollmentAsync(int studentId)
        {
            var enrollment = await _context.Enrollments.FirstOrDefaultAsync(e => e.PersonId == studentId && e.RoleType == RoleTypes.STUDENT);
            return enrollment != null ? Ok(enrollment) : NotFound(enrollment);
        }

        [HttpGet("GetAllEnrolls")]
        public async Task<ActionResult<Enrollment>> GetAllEnrollsAsync()
        {
            var enrollments = await _context.Enrollments.ToListAsync();
            return !(enrollments.Any()) ? NotFound("No Enrollements") : Ok(enrollments);
        }

        [HttpPost("AddStudentEnrollement")]
        public async Task<IActionResult> AddEnrollment([FromBody] Enrollment enrollment)
        {
            var newEnroll = await _context.Enrollments.AnyAsync(e => e.PersonId == enrollment.PersonId && e.RoleType == RoleTypes.STUDENT);
            if (newEnroll)
            {
                return BadRequest($"{enrollment.PersonId} already Enrolled");
            }
            _context.Enrollments.Add(enrollment);
            await _context.SaveChangesAsync();
            return CreatedAtAction("New Enrollment", new {enrollment.Id });
        }

        [HttpPut("EditStudentEnrollment/{studentId}")]
        public async Task<ActionResult> EditEnrollment(int studentId,[FromBody]Enrollment enroll)
        {
            try
            {
                var edit = await _context.Enrollments.FirstOrDefaultAsync(e => e.PersonId == studentId && e.RoleType == RoleTypes.STUDENT);
                if (edit != null)
                {
                    edit.EnrollmentDate = enroll.EnrollmentDate;
                    _context.Enrollments.Update(edit);
                    await _context.SaveChangesAsync();
                    return Ok(edit);
                }
                return NotFound($"{studentId}, Not Found");
            }
            catch (Exception ex) {
                throw ex;
            }
        }
       
        [HttpDelete("DeleteStudentEnrollment{studentId}")]
        public async Task<IActionResult> DeleteEnrollment(int studentId)
        {
            var delete = await _context.Enrollments.FirstOrDefaultAsync(en => en.PersonId == studentId && en.RoleType == RoleTypes.STUDENT);
            if (delete != null)
            {
                
                _context.Enrollments.Remove(delete);
                await _context.SaveChangesAsync();
                return Ok($"{studentId} Enrollment Deleted ");
            }
            return NotFound($"{studentId}, Not Found");
        }
    }
}
