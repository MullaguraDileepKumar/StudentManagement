using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Core.Data;
using WebApplication2.Core.Model;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GradesController(ApplicationDbContext _context) : ControllerBase
    {
        [HttpGet("GetStudentGrade/{studentId}")]
        public async Task<ActionResult<Enrollment>> GetEnrollmentAsync(int studentId)
        {
            var student = await _context.Students.FirstOrDefaultAsync(e => e.Id == studentId);
            if(student != null)
            {
                var getGradeValue = await _context.Grades.FirstOrDefaultAsync(g => g.Id == student.GradeId);
                return Ok(getGradeValue);
            }
            return NotFound();
        }

        [HttpGet("GetAllGrades")]
        public async Task<ActionResult<Enrollment>> GetAllGrades()
        {
            var grades = await _context.Grades.ToListAsync();
            return !(grades.Any()) ? NotFound("No grades") : Ok(grades);
        }
        
        [HttpPost]
        [Route("AddGrade")]
        public async Task<IActionResult> AddGrade([FromBody] Grade grade)
        {
            var newGrade = await _context.Grades.AnyAsync(e => e.GradeValue == grade.GradeValue);
            if (newGrade)
            {
                return BadRequest("grade already Exist");
            }
            _context.Grades.Add(grade);
            await _context.SaveChangesAsync();
            return Ok("New Grade created");
        }

        [HttpPut("EditStudentGrade/{studentId}/{gradeId}")]
        public async Task<ActionResult> EditStudentGrade(int studentId, int gradeId)
        {
            var edit = await _context.Grades.FirstOrDefaultAsync(e => e.Id == gradeId);
            if (edit != null)
            {
                var student = await _context.Students.FirstOrDefaultAsync(e => e.Id == studentId && e.GradeId == edit.Id);
                if (student != null) {
                    student.GradeId = edit.Id;
                    _context.Students.Update(student);
                    await _context.SaveChangesAsync();
                    return Ok($"{student} grade updated");
                }
                return NotFound($"Student doesn't has this grade");
            }
            return NotFound($"{gradeId}, Grade Not Found");
        }

        [HttpDelete("DeleteGrade{gradeId}")]
        public async Task<IActionResult> DeleteGrade(int gradeId)
        {
            try
            {
                var deleteGrade = await _context.Grades.FirstOrDefaultAsync(en => en.Id == gradeId);
                if (deleteGrade != null)
                {
                    var isGradeFoundInStudent = await _context.Students.AnyAsync(en => en.GradeId == gradeId);
                    if (isGradeFoundInStudent)
                    {
                        return BadRequest("Grades Associated with Student");
                    }
                    else
                    {
                        _context.Grades.Remove(deleteGrade);
                        await _context.SaveChangesAsync();
                        return Ok("Grade Deleted");
                    }
                }
                return NotFound($"{gradeId}, Not Found");
            }
            catch (Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
        }
    }
}
