using Microsoft.AspNetCore.Identity;

namespace WebApplication2.Core.Interfaces
{
    public interface IRoleService
    {
        Task<IdentityRole?> GetRole(string id);
        Task<IEnumerable<IdentityRole>> GetRoleList();
        Task<IdentityRole> AddRole(IdentityRole college);
        Task<IdentityRole> EditRole(string id, IdentityRole college);
        Task<bool> DeleteRole(string id);
    }
}
