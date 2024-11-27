using Microsoft.AspNetCore.Mvc;
using WebApplication2.Core.Dtos;
using WebApplication2.Core.Dtos.General;
using WebApplication2.Core.Model;

namespace WebApplication2.Core.Interfaces
{
    public interface ICollegeService
    {
        Task<College?> GetCollege(int id);
        Task<IEnumerable<College>> GetCollegeList();
        Task<College> AddCollege(College college);
        Task<College> EditCollege(int id,College college);
        Task<bool> DeleteCollege(int id);
        Task<IEnumerable<StudentView>> GetStudentsInCollege(int id);
    }
}
