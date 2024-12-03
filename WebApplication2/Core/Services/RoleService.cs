using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Core.Data;
using WebApplication2.Core.Interfaces;

namespace WebApplication2.Core.Services
{
    public class RoleService(ApplicationDbContext _context) : IRoleService
    {
        public async Task<IdentityRole?> AddRole(IdentityRole role)
        {
            try
            {
                role.Id = Guid.NewGuid().ToString();
                role.ConcurrencyStamp = Guid.NewGuid().ToString();
                bool isRoleExist = await _context.AspNetRoles.AnyAsync(r => r.Name == role.Name);
                if (!isRoleExist)
                {
                    await _context.AspNetRoles.AddAsync(role);
                    await _context.SaveChangesAsync();
                    return role;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> DeleteRole(string name)
        {
            try
            {
                bool result = false;
                var aspNetRoles = await _context.AspNetRoles.FirstOrDefaultAsync(m => m.Name == name);
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

        public async Task<IdentityRole> EditRole(string name, IdentityRole role)
        {
            try
            {
                var prevRole = await _context.AspNetRoles.FirstOrDefaultAsync(c => c.Name == name);
                if (prevRole != null)
                {
                    prevRole.Name = role.Name.ToUpper();
                    prevRole.NormalizedName = role.NormalizedName.ToUpper();

                    await _context.SaveChangesAsync();
                    return prevRole;
                }
                return null;
            }
            catch (Exception ex) { throw ex; }
        }

        public async Task<IdentityRole?> GetRole(string name)
        {
            try
            {
                var aspNetRoles = await _context.AspNetRoles
                .FirstOrDefaultAsync(m => m.Name.ToUpper() == name.ToUpper());
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
                        NormalizedName = q.NormalizedName,
                        ConcurrencyStamp = q.ConcurrencyStamp,
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
