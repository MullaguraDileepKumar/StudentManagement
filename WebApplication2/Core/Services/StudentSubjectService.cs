using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Core.Data;
using WebApplication2.Core.Dtos;
using WebApplication2.Core.Interfaces;
using WebApplication2.Core.Model;

namespace WebApplication2.Core.Services
{
    public class StudentSubjectService(ApplicationDbContext _context) : IStudentSubjectService
    {
        public async Task<StudentSubject> AddStudentSubject(StudentSubject studentSubject)
        {
            try
            {
                    await _context.StudentSubjects.AddAsync(studentSubject);
                await _context.SaveChangesAsync();
                    return studentSubject;
    
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<StudentSubject>> GetStudentSubjects(int studentId)
        {
            var listSubjects = await _context.StudentSubjects.Where(ss => ss.StudentId == studentId).ToListAsync();
            if (listSubjects.Count > 0)
                return listSubjects;
            return null;
        }
    }
}
