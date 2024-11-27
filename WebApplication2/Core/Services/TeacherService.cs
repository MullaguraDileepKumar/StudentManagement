using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Core.Data;
using WebApplication2.Core.Interfaces;
using WebApplication2.Core.Model;

namespace WebApplication2.Core.Services
{
    public class TeacherService(ApplicationDbContext _context) :ITeacherService
    {
        public async Task<IEnumerable<Teacher>> GetAllTeachers()
        {
            var teachersList = await _context.Teachers.ToListAsync();
            return teachersList;
        }
        public async Task<bool> AddTeacher(Teacher teacher)
        {
            var newTeacher = await _context.AddAsync(teacher);
            await _context.SaveChangesAsync();
            return newTeacher != null;
        }
        public async Task<Teacher?> EditTeacher(int id,Teacher teacher)
        {
            var edited = await _context.Teachers.FirstOrDefaultAsync(t => t.Id == id);
             if(edited != null)
            {
                edited.Name = teacher.Name;
                edited.DOB = teacher.DOB;
                edited.Email = teacher.Email;
                edited.PhoneNumber = teacher.PhoneNumber;
                edited.HireDate = teacher.HireDate;
                edited.Salary = teacher.Salary;
                await _context.SaveChangesAsync();
                return edited;
            }
             else
            {
                return null;
            }
        }
        public async Task<bool> DeleteTeacher(int id)
        {
            try
            {

                bool result = false;
                var teacher = await _context.Teachers.FirstOrDefaultAsync(t => t.Id == id);
                if (teacher != null)
                {
                    _context.Teachers.Remove(teacher);
                    await _context.SaveChangesAsync();
                    result = true;
                }
                return result;
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
