using Microsoft.AspNetCore.Mvc;
using WebApplication2.Core.Interfaces;
using WebApplication2.Core.Model;
using WebApplication2.Core.Services;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SemesterController(ISemesterService _semesterService) : Controller
    {
        [HttpGet("GetSemester{id}")]
        public async Task<ActionResult<Department>> GetSemester(int id)
        {
            var dept = await _semesterService.GetSemester(id);
            return Ok(dept);
        }

        [HttpGet("GetAllSemesters")]
        public async Task<ActionResult<Semester>> GetAllSemesters()
        {
            var deptList = await _semesterService.GetAllSemesters();
            return Ok(deptList);
        }

        [HttpPost("AddSemester")]
        public async Task<ActionResult<Semester>> AddSemester([FromBody] Semester sem)
        {
            var newDept = await _semesterService.AddSemester(sem);
            return newDept != null ? Ok(newDept) : BadRequest();
        }

        [HttpPut("EditSemester/{id}")]
        public async Task<ActionResult<Semester>> EditSemester(int id, Semester sem)
        {
            var editedDept = await _semesterService.EditSemester(id, sem);
            return Ok(editedDept);
        }

        [HttpDelete("DeleteSemester{id}")]
        public async Task<IActionResult> DeleteSemester(int id)
        {
            bool isDeleted = await _semesterService.DeleteSemester(id);
            return isDeleted ? Ok() : BadRequest();
        }
    }
}
