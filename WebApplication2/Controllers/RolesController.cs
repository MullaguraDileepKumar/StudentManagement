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

        
        [HttpGet("GetRole/{id}")]
        public async Task<ActionResult<IdentityRole>> GetRole(string id)
        {
                var roleData = await _roleService.GetRole(id);
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
                return Ok(response);
            }
            return BadRequest();
        }

        [HttpPut("EditRole")]
        public async Task<ActionResult<IdentityRole>> EditRole(string id, IdentityRole role)
        {
            if (role != null)
            {
                var response = await _roleService.EditRole(id, role);
                return Ok(response);
            }
            return BadRequest();
        }

        [HttpDelete("DeleteRole/{id}")]
        public async Task<IActionResult> DeleteRole(string id)
        {
                bool response = await _roleService.DeleteRole(id);
                if (response)
                    return Ok("Role Deleted");
                else
                    return NotFound("No Role Found");
        }
    }
}
