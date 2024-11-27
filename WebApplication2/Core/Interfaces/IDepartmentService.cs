using Microsoft.AspNetCore.Mvc;
using WebApplication2.Core.Model;

namespace WebApplication2.Core.Interfaces
{
    public interface IDepartmentService
    {
        Task<Department?> GetDepartment(int id);
        Task<IEnumerable<Department>> GetAllDepartments();
        Task<Department> AddDepartment(Department dept);
        Task<Department> EditDepartment(int id, Department dept);
        Task<bool> DeleteDepartment(int id);
    }
}
