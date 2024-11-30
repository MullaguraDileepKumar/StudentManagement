using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Core.Constants;
using WebApplication2.Core.Data;
using WebApplication2.Core.Dtos;
using WebApplication2.Core.Interfaces;
using WebApplication2.Core.Model;

namespace WebApplication2.Core.Services
{
    public class StudentService(ApplicationDbContext _context,ILogger<StudentService> _logger):IStudentService
    {
        public async Task<IEnumerable<Student>> GetAllStudents()
        {
            var studentList = await _context.Students.ToListAsync();
            return studentList;
        }

        
        public IQueryable<StudentView> GetStudentDetail(int id)
        {
            try
            {
                var student = from s in _context.Students
                              join c in _context.College
                              on s.CollegeId equals c.Id
                              join e in _context.Enrollments
                              on s.Id equals e.PersonId
                              where s.Id == id && (s.Id == e.PersonId && e.RoleType == RoleTypes.STUDENT)
                              select new StudentView
                              {
                                  Student = new Student 
                                  {
                                      Id = s.Id,
                                      Name = s.Name,
                                      PhoneNumber = s.PhoneNumber,
                                      EnrollmentId = e.Id,
                                  },
                                  College = new College
                                  {
                                      Id = c.Id,
                                      Name = c.Name,
                                      Address = c.Address,
                                  }
                              };
                if (student != null ) 
                    return student.AsQueryable();
                else
                    return new StudentView[0].AsQueryable();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during Getting Student Details");
                throw;
            }
        }

        
        /*public async Task<Student?> Details(int id)
        {

            try
            {
                var studentDetail = await _context.StudentDeatails
                .FirstOrDefaultAsync(m => m.Id == id);
                if (studentDetail != null)
                {
                    return studentDetail;
                }
                return null;
            }
            catch (Exception ex) { throw ex; }
        }*/

        
        public async Task<Student> AddStudent(Student student)
        {
            try
            {
                await _context.Students.AddAsync(student);
                await _context.SaveChangesAsync();
                return student;
            }
            catch (Exception ex) { throw ex; }
        }


        
        public async Task<Student?> Editstudent(int? id, [FromBody] Student student)
        {
                try
                {
                    var studentDetail = await _context.Students.FindAsync(id);
                    if (studentDetail != null) 
                    {
                        if (!string.IsNullOrEmpty(student.Name) && student.Name != "string")
                            studentDetail.Name = student.Name;
                        if (!string.IsNullOrEmpty(student.PhoneNumber) && student.PhoneNumber != "string")
                            studentDetail.PhoneNumber = student.PhoneNumber;
                        await _context.SaveChangesAsync();
                    }
                     return studentDetail;
            
                }
                catch (Exception ex)
                {
                    throw ex;
                }
        }

        public async Task<bool> DeleteStudent(int id)
        {
            try
            {
                bool result = false;
                var studentDetail = await _context.Students
                    .FirstOrDefaultAsync(m => m.Id == id);
                if (studentDetail != null)
                {
                    _context.Students.Remove(studentDetail);
                    await _context.SaveChangesAsync();
                    result = true;
                }
                return result;
            }catch (Exception ex) { throw ex; }
        }
    }
}
