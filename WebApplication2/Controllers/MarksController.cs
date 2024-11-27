using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebApplication2.Core.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MarksController(IMarksService _marksService) : ControllerBase
    {
       
        [HttpGet("GetStudentAllMarks/{id}")]
        public async Task<IActionResult> GetStudentAllMarks(int id)
        {
            var studentAllMarks =  _marksService.GetStudentAllMArks(id);
            if (studentAllMarks != null)
            {
                return Ok(JsonConvert.SerializeObject(studentAllMarks));
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost("Add/Edit-SubjectMarks/{studentId}/{subjectId}/{marks}")]
        public async Task<IActionResult> AddSubjectMarks(int studentId,int subjectId,int marks)
        {
            var newMarks =await  _marksService.AddSubjectMarks(studentId, subjectId, marks);
            return Ok(newMarks);
        }

        [HttpDelete("DeleteStudentMarks/{studentId}/{subjectId}")]
        public IActionResult DeleteStudentMarks(int studentId, int subjectId)
        { 
            var isDeleted =  _marksService.DeleteStudentMarks(studentId, subjectId);
            if (isDeleted)
                return Ok("Student particular subject marks deleted");
            else
                return BadRequest("No record found");
        }
    }
}
