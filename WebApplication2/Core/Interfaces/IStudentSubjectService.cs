using Microsoft.AspNetCore.Mvc;
using WebApplication2.Core.Model;

namespace WebApplication2.Core.Interfaces
{
    public interface IStudentSubjectService
    {
        Task<StudentSubject> AddStudentSubject(StudentSubject studentSubject);
        Task<IEnumerable<StudentSubject>> GetStudentSubjects(int studentId);
    }
}
