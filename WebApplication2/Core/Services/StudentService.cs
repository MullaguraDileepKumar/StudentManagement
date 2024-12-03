using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.FlowAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
                              join t in _context.Teachers
                              on s.DepartmentId equals t.DepartmentId
                              join d in _context.Departments
                              on s.DepartmentId equals d.Id
                              join sm in _context.Semesters
                              on s.SemesterId equals sm.Id
                              where s.Id == id && (s.Id == e.PersonId && e.RoleType == RoleTypes.STUDENT)
                              select new StudentView
                              {
                                  Student = new Student 
                                  {
                                      Id = s.Id,
                                      Name = s.Name,
                                      PhoneNumber = s.PhoneNumber,
                                  },
                                  College = new College
                                  {
                                      Id = c.Id,
                                      Name = c.Name,
                                      Address = c.Address,
                                  },
                                  Teacher = new Teacher
                                  {
                                      Name = t.Name,
                                      PhoneNumber= t.PhoneNumber,
                                      DOB = t.DOB,
                                      HireDate = t.HireDate,
                                  },
                                  Department = new Department
                                  {
                                      Id = d.Id,
                                      Name = d.Name,
                                  },
                                  Semester = new Semester
                                  {
                                      Id = sm.Id,
                                      Name = sm.Name,
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

        
        public async Task<Student?> AddStudent(Student student)
        {
            try
            {
                if (!(student.PhoneNumber.IsNullOrEmpty() || student.Name.IsNullOrEmpty()))
                {
                    await _context.Students.AddAsync(student);
                    await _context.SaveChangesAsync();
                    return student;
                }
                return null;
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
