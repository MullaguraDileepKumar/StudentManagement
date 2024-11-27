using Microsoft.AspNetCore.Mvc;
using WebApplication2.Core.Dtos;
using WebApplication2.Core.Model;

namespace WebApplication2.Core.Interfaces
{
    public interface IMarksService
    {
        IQueryable<MarksView> GetStudentAllMArks(int id);
        Task<Marks> AddSubjectMarks(int studentId, int subjectId, int marks);
        bool DeleteStudentMarks(int studentId, int subjectId);
    }
}
