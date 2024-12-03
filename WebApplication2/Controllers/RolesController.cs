using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Core.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController(IRoleService _roleService) : ControllerBase
    {
        [HttpGet("GetRoleList")]
        public async Task<IEnumerable<IdentityRole>> GetRoleList()
        {
            var list = await _roleService.GetRoleList();
            return list;
        }

        
        [HttpGet("GetRole/{name}")]
        public async Task<ActionResult<IdentityRole>> GetRole(string name)
        {
                var roleData = await _roleService.GetRole(name);
                if (roleData != null)
                    return Ok(roleData);
                else
                    return NotFound("Role Not Found");
        }

        [HttpPost("AddRole")]
        public async Task<ActionResult<IdentityRole>> AddRole([FromBody] IdentityRole role)
        {
            if (role != null)
            {
                var response = await _roleService.AddRole(role);
                if (response != null)
                    return Ok(response);
                else return BadRequest($"{role.Name} Already exists");
            }
            return BadRequest();
        }

        [HttpPut("EditRole")]
        public async Task<ActionResult<IdentityRole>> EditRole(string name, IdentityRole role)
        {
            if (role != null)
            {
                var response = await _roleService.EditRole(name, role);
                return response != null ?  Ok(response) :  NotFound($"{name} No Role Found");
            }
            return BadRequest();
        }

        [HttpDelete("DeleteRole/{name}")]
        public async Task<IActionResult> DeleteRole(string name)
        {
                bool response = await _roleService.DeleteRole(name);
                if (response)
                    return Ok("Role Deleted");
                else
                    return NotFound("No Role Found");
        }
    }
}
