using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Core.Data;
using WebApplication2.Core.Interfaces;

namespace WebApplication2.Core.Services
{
    public class RoleService(ApplicationDbContext _context) : IRoleService
    {
        public async Task<IdentityRole> AddRole(IdentityRole role)
        {
            try
            {
                await _context.AspNetRoles.AddAsync(role);
                await _context.SaveChangesAsync();
                return role;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteRole(string id)
        {
            try
            {
                bool result = false;
                var aspNetRoles = await _context.AspNetRoles.FirstOrDefaultAsync(m => m.Id == id);
                if (aspNetRoles != null)
                {
                    _context.AspNetRoles.Remove(aspNetRoles);
                    await _context.SaveChangesAsync();
                    result = true;
                }
                return result;
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<IdentityRole> EditRole(string id, IdentityRole role)
        {
            try
            {
                var prevRole = await _context.AspNetRoles.FirstOrDefaultAsync(c => c.Id == id);
                if (prevRole != null)
                {
                    prevRole.Name = role.Name.ToUpper();
                    prevRole.NormalizedName = role.NormalizedName.ToUpper();
                    await _context.SaveChangesAsync();
                }
                return prevRole;
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<IdentityRole?> GetRole(string id)
        {
            try
            {
                var aspNetRoles = await _context.AspNetRoles
                .FirstOrDefaultAsync(m => m.Id == id);
                if (aspNetRoles == null)
                    return null;
                return aspNetRoles;
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<IEnumerable<IdentityRole>> GetRoleList()
        {
            try
            {
                var aspNetRolesList = await _context.AspNetRoles
                    .Select(q => new IdentityRole
                    {
                        Id = q.Id,
                        Name = q.Name,
                    }).ToListAsync();

                return aspNetRolesList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
