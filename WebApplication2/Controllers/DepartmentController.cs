using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Core.Interfaces;
using WebApplication2.Core.Model;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController(IDepartmentService _departmentService) : ControllerBase
    {
        [HttpGet("GetDepartment{id}")]
        public async Task<ActionResult<Department>> GetDepartment(int id)
        {
            var dept = await _departmentService.GetDepartment(id);
            return Ok(dept);
        }

        [HttpGet("GetAllDepartments")]
        public async Task<ActionResult<Department>> GetAllDepartments()
        {
            var deptList = await _departmentService.GetAllDepartments();
            return Ok(deptList);
        }

        [HttpPost("AddDepartment")]
        public async Task<ActionResult<Department>> AddDepartment([FromBody] Department dept)
        {
            var newDept = await _departmentService.AddDepartment(dept);
            return newDept != null ? Ok(newDept) : BadRequest();
        }

        [HttpPut("EditDepartment/{id}")]
        public async Task<ActionResult<Department>> EditDepartment(int id, Department dept)
        {
            var editedDept = await _departmentService.EditDepartment(id,dept);
            return Ok(editedDept);
        }

        [HttpDelete("DeleteDepartment{id}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            bool isDeleted = await _departmentService.DeleteDepartment(id);
            return isDeleted ? Ok() : BadRequest();
        }
    }
}
