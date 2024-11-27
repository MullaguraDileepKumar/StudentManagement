using Microsoft.AspNetCore.Mvc;
using WebApplication2.Core.Dtos;
using WebApplication2.Core.Model;

namespace WebApplication2.Core.Interfaces
{
    public interface IStudentService
    {
        Task<IEnumerable<Student>> GetAllStudents();
        IQueryable<StudentView> GetStudentDetail(int id);
        Task<Student> AddStudent(Student student);
        Task<Student?> Editstudent(int? id, [FromBody] Student student);
        Task<bool> DeleteStudent(int id);
    }
}
