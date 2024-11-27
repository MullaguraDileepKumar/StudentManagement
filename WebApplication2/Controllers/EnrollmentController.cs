using Microsoft.AspNetCore.Mvc;
using WebApplication2.Core.Interfaces;
using WebApplication2.Core.Model;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Core.Data;
using Microsoft.IdentityModel.Tokens;
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
            var enrollment = await _context.Enrollments.FirstOrDefaultAsync(e => e.StudentId == studentId);
            return enrollment != null ? Ok(enrollment) : NotFound(enrollment);
        }

        [HttpGet("GetAllEnrolls")]
        public async Task<ActionResult<Enrollment>> GetAllEnrollsAsync()
        {
            var enrollments = await _context.Enrollments.ToListAsync();
            return !(enrollments.Any()) ? NotFound("No Enrollements") : Ok(enrollments);
        }

        [HttpPost("AddEnrollement")]
        public async Task<IActionResult> AddEnrollment([FromBody] Enrollment enrollment)
        {
            var newEnroll = await _context.Enrollments.AnyAsync(e => e.StudentId == enrollment.StudentId);
            if (newEnroll)
            {
                return BadRequest($"{enrollment.StudentId} already Enrolled");
            }
            _context.Enrollments.Add(enrollment);
            await _context.SaveChangesAsync();
            return CreatedAtAction("New Enrollment", new {enrollment.Id });
        }

        [HttpPut("EditEnrollment/{studentId}")]
        public async Task<ActionResult> EditEnrollment(int studentId,[FromBody]Enrollment enroll)
        {
            var edit = await _context.Enrollments.FirstOrDefaultAsync(e => e.StudentId == studentId);
            if (edit != null)
            {
                edit.EnrollmentDate = enroll.EnrollmentDate;
                 _context.Update(edit);
                await _context.SaveChangesAsync();
                return Ok(edit);
            }
            return NotFound($"{studentId}, Not Found");
        }
       
        [HttpDelete("DeleteEnrollment{studentId}")]
        public async Task<IActionResult> DeleteEnrollment(int studentId)
        {
            var delete = await _context.Enrollments.FirstOrDefaultAsync(en => en.StudentId == studentId);
            if (delete != null)
            {
                
                _context.Update(delete);
                await _context.SaveChangesAsync();
                return Ok($"{studentId} Enrollment Deleted ");
            }
            return NotFound($"{studentId}, Not Found");
        }
    }
}
