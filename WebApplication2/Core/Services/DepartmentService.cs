using Microsoft.AspNetCore.Mvc;
using WebApplication2.Core.Data;
using WebApplication2.Core.Interfaces;
using WebApplication2.Core.Model;
using Microsoft.EntityFrameworkCore;

namespace WebApplication2.Core.Services
{
    public class DepartmentService(ApplicationDbContext _context) : IDepartmentService
    {
        public async Task<Department?> GetDepartment(int id)
        {
            var dept = await _context.Departments.FindAsync(id);
            return dept ?? null;     
        }

        public async Task<IEnumerable<Department>> GetAllDepartments()
        {
            try
            {
                var list = await (from dept in _context.Departments
                            select new Department
                            {
                                Id = dept.Id,
                                Name = dept.Name,
                            }).ToListAsync();
                return list;
            }
            catch (Exception ex) { throw ex; }
        }
        public async Task<Department> AddDepartment(Department dept)
        {
            try
            {
                await _context.Departments.AddAsync(dept);
                await _context.SaveChangesAsync();
                return dept;
            }
            catch (Exception ex) { throw ex; }
        }
        public async Task<Department> EditDepartment(int id,Department dept)
        {
            var editedDept = await _context.Departments.FirstOrDefaultAsync(d => d.Id == id);
            if(editedDept != null)
            {
                editedDept.Name = dept.Name;
                await _context.SaveChangesAsync();
                return editedDept;
            }
            return editedDept;
        }
        public async Task<bool> DeleteDepartment(int id)
        {
            bool result = false;
            var record =  _context.Departments.FirstOrDefaultAsync(d => d.Id == id);
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
