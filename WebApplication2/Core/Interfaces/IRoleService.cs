using Microsoft.AspNetCore.Identity;

namespace WebApplication2.Core.Interfaces
{
    public interface IRoleService
    {
        Task<IdentityRole?> GetRole(string name);
        Task<IEnumerable<IdentityRole>> GetRoleList();
        Task<IdentityRole?> AddRole(IdentityRole college);
        Task<IdentityRole> EditRole(string name, IdentityRole college);
        Task<bool> DeleteRole(string name);
    }
}
