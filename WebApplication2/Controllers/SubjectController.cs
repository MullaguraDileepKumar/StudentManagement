using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Core.Interfaces;
using WebApplication2.Core.Model;

namespace WebApplication2.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController(ISubjectService _subjectService) : ControllerBase
    {

        [HttpGet("GetAllSubjects")]
        public async Task<IEnumerable<Subject>> GetSubjectList()
        {
            var list = await _subjectService.GetSubjectList();
            return list;
        }

        [HttpGet("GetSubject/{id}")]
        public async Task<ActionResult<College>> GetSubject(int id)
        {
            if (id > 0)
            {
                var subjectData = await _subjectService.GetSubject(id);
                if (subjectData != null)
                    return Ok(subjectData);
                else
                    return NotFound("Subject Not Found");

            }
            return BadRequest();
        }

        [HttpPost("AddSubject")]
        public async Task<ActionResult<Subject>> AddSubject([FromBody] Subject subject)
        {
            if (subject != null)
            {
                var response = await _subjectService.AddSubject(subject);
                return Ok(response);
            }
            return BadRequest();
        }

        [HttpPut("EditSubject")]
        public async Task<ActionResult<Subject>> EditSubject(int id, Subject subject)
        {
            if (subject != null)
            {
                var response = await _subjectService.EditSubject(id, subject);
                return Ok(response);
            }
            return BadRequest();
        }

        [HttpDelete("DeleteSubject/{id}")]
        public async Task<IActionResult> DeleteSubject(int id)
        {
            if (id > 0)
            {
                bool response = await _subjectService.DeleteSubject(id);
                if (response)
                    return Ok("Subject Deleted");
                else
                    return NotFound("No Subject Found");
            }
            return BadRequest();
        }
    }
}
