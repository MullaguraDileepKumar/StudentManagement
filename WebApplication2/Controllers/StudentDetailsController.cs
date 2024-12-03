using Microsoft.AspNetCore.Mvc;
using WebApplication2.Core.Constants;
using WebApplication2.Core.Data;
using WebApplication2.Core.Dtos;
using WebApplication2.Core.Interfaces;
using WebApplication2.Core.Model;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentDetailsController(IStudentService _studentService) : ControllerBase
    {

        [HttpGet("GetAllStudents")]
        public async Task<IEnumerable<Student>> GetAllStudents()
        {
            var list = await _studentService.GetAllStudents();
            return list;
        }

        // GET: GetStudentDetail
        [HttpGet("GetStudentDetail/{id}")]
        public IQueryable<StudentView> GetStudentDetail(int id)
        {
            var studentData = _studentService.GetStudentDetail(id);
            return studentData;
        }

        [HttpPost("AddStudent")]
        public async Task<ActionResult<Student>> AddStudent([FromBody] Student student)
        {
            if (student != null)
            {
                //student.StatusId = StudentStatusEnums.ACTIVE;
                var response = await _studentService.AddStudent(student);
                return response != null ? Ok(response) : BadRequest("Please Fill Required fields");
            }
            return BadRequest();
        }

        [HttpPut("Editstudent")]
        public async Task<ActionResult<College>> Editstudent(int id, Student student)
        {
            if (student != null)
            {
                var response = await _studentService.Editstudent(id, student);
                return Ok(response);
            }
            return BadRequest();
        }

        [HttpDelete("DeleteStudent/{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            if (id > 0)
            {
                bool response = await _studentService.DeleteStudent(id);
                if (response)
                    return Ok("Student Deleted");
                else
                    return NotFound("No Student Found");
            }
            return BadRequest();
        }



       /* [HttpPost("AddMarks")]
        public async Task<IActionResult> AddMarks(int id,[FromBody] MarksData marksData)
        {
            if (marksData == null)
            {
                return BadRequest();
            }
            
            var marks = new Marks
            {
                MarksData = JsonConvert.SerializeObject(marksData)
            };
            _context.Marks.Add(marks);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetMarks), new { id = marks.Id }, marks);
        }*/
       /* [HttpGet("GetMarks/{id}")]
        public async Task<IActionResult> GetMarks(int id)
        {
            var marks = await _context.Marks.FindAsync(id);

            if (marks == null)
            {
                return NotFound();
            }

            // Deserialize the JSON string back to MarksData object
            var marksData = JsonConvert.DeserializeObject<MarksData>(marks.MarksData);

            return Ok(marksData);  // Return the deserialized object
        }*/

       /* [HttpGet("GetStudentMarks/{studentid}")]
        public async Task<IActionResult> GetStudentMarks(int studentid)
        {

            var student = from s in _context.Students
                          join m in m.
            var marks = await _context.Marks.ToListAsync();
            var filteredMarks = marks.Select(m => new
            {
                MarksData = JsonConvert.DeserializeObject<MarksData>(m.MarksData)
            })
                .Where(m => m.MarksData.StudentId == studentid)
                .Select(m => m.MarksData).ToList();
            var studentData = new StudentView
            {
                Id = 
            }
            return Ok(filteredMarks);  
        }*/
    }
}
