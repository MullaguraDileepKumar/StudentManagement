using Microsoft.EntityFrameworkCore;
using WebApplication2.Core.Data;
using WebApplication2.Core.Interfaces;
using WebApplication2.Core.Model;

namespace WebApplication2.Core.Services
{
    public class SemesterService(ApplicationDbContext _context) : ISemesterService
    {
        public async Task<Semester?> GetSemester(int id)
        {
            var dept = await _context.Semesters.FindAsync(id);
            return dept ?? null;
        }

        public async Task<IEnumerable<Semester>> GetAllSemesters()
        {
            try
            {
                var list = await (from sem in _context.Semesters
                                  select new Semester
                                  {
                                      Id = sem.Id,
                                      Name = sem.Name,
                                  }).ToListAsync();
                return list;
            }
            catch (Exception ex) { throw ex; }
        }
        public async Task<Semester> AddSemester(Semester sem)
        {
            try
            {
                await _context.Semesters.AddAsync(sem);
                return sem;
            }
            catch (Exception ex) { throw ex; }
        }
        public async Task<Semester> EditSemester(int id, Semester sem)
        {
            var editedsem = await _context.Semesters.FirstOrDefaultAsync(d => d.Id == id);
            if (editedsem != null)
            {
                editedsem.Name = sem.Name;
                await _context.SaveChangesAsync();
                return editedsem;
            }
            return editedsem;
        }
        public async Task<bool> DeleteSemester(int id)
        {
            bool result = false;
            var record = _context.Semesters.FirstOrDefaultAsync(d => d.Id == id);
            if (record != null)
            {
                _context.Remove(record);
                result = true;
            }
            await _context.SaveChangesAsync();
            return result;
        }
    }
}
