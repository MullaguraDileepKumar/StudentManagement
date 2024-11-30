using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mysqlx;
using WebApplication2.Core.Data;
using WebApplication2.Core.Dtos;
using WebApplication2.Core.Interfaces;
using WebApplication2.Core.Model;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CollegesController : ControllerBase
    {
        private readonly ICollegeService _collegeService;

        public CollegesController(ICollegeService collegeService)
        {
            _collegeService = collegeService;
        }

        [HttpGet("GetAllColleges")]
        public async Task<IEnumerable<College>> GetCollegesList()
        {
            var list = await _collegeService.GetCollegeList();
            return list;
        }

        // GET: College by Id
        [HttpGet("GetCollege/{id}")]
        public async Task<ActionResult<College>> GetCollege(int id)
        {
            if (id > 0)
            {
                var collegeData = await _collegeService.GetCollege(id);
                if (collegeData != null)
                    return Ok(collegeData);
                else
                    return NotFound("College Not Found");
            }
            return BadRequest();
        }

        [HttpPost("AddCollege")]
        public async Task<ActionResult<College>> AddCollege([FromBody] College college)
        {
            if (college != null)
            {
                var response = await _collegeService.AddCollege(college);
                return Ok(response);
            }
            return BadRequest();
        }

        [HttpPut("EditCollege")]
        public async Task<ActionResult<College>> EditCollege(int id,College college)
        {
            if (college != null)
            {
                var response = await _collegeService.EditCollege(id,college);
                return Ok(response);
            }
            return BadRequest();
        }

        [HttpDelete("DeleteCollege/{id}")]
        public async Task<IActionResult> DeleteCollege(int id)
        {
            try
            {
                if (id > 0)
                {
                    bool response = await _collegeService.DeleteCollege(id);
                    if (response)
                        return Ok("College Deleted");
                    else
                        return NotFound("No College Found");
                }
            }
            catch (Exception ex) { throw ex; }
            return BadRequest();
        }

        [HttpGet("GetStudentsInCollege/{id}")]
        public async Task<IEnumerable<StudentView>> GetStudentsInCollege(int id)
        {
            if (id > 0)
                return await _collegeService.GetStudentsInCollege(id);
            else
                return Enumerable.Empty<StudentView>();
             
        }
    }
}
