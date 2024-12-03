using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Core.Data;
using WebApplication2.Core.Interfaces;
using WebApplication2.Core.Model;


namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentSubjectController(IStudentSubjectService _studentSubjectService) : ControllerBase
    {

        [HttpGet("GetStudentSubjects{studentId}")]
        public async Task<ActionResult<List<StudentSubject>>> GetStudentSubjects(int studentId)
        {
            var listSubjects = await _studentSubjectService.GetStudentSubjects(studentId);
            return Ok(listSubjects);
        }

        [HttpPost("AddStudentSubject")]
        public async Task<ActionResult<StudentSubject>> AddStudentSubject([FromBody] StudentSubject studentSubject)
        {
            try
            {
                if (studentSubject == null)
                    return NoContent();
                else
                {
                    var respose = await _studentSubjectService.AddStudentSubject(studentSubject);
                    return Ok(respose);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
