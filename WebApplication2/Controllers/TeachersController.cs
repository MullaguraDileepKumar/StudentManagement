using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Core.Interfaces;
using WebApplication2.Core.Model;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersController : ControllerBase
    {
        private readonly ITeacherService _teacherService;
        public TeachersController(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }
        [HttpGet("GetAllTeachers")]
        public async Task<IEnumerable<Teacher>> GetAllTeachers()
        {
            var list = await _teacherService.GetAllTeachers();
            return list;
        }

        [HttpPost("AddTeacher")]
        public async Task<IActionResult> AddTeacher(Teacher teacher)
        {
            var isAdded = await _teacherService.AddTeacher(teacher);
            if(isAdded)
                return Ok(teacher);
            else return BadRequest();
        }

        [HttpPut("EditTeacher/{id}")]
        public async Task<ActionResult<Teacher>> EditTeacher(int id, [FromBody] Teacher teacher)
        {
            var edited = await _teacherService.EditTeacher(id,teacher);
            if (edited != null)
                return Ok(edited);
            else
                return BadRequest();
        }
        [HttpDelete("DeleteTeacher/{id}")]
        public async Task<ActionResult<bool>> DeleteTeacher(int id)
        {
            var isDeleted = await _teacherService.DeleteTeacher(id);
            if (isDeleted)
                return Ok("Teacher Deleted");
            else
                return BadRequest("Something went wrong");
        }
    }
}
