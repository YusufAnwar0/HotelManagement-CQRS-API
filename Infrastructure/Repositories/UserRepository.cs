using Domain.IRepositories;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class UserRepository : GeneralRepository<User>, IUserRepository
    {
        public UserRepository(Context context) : base(context)
        {
        }

        public async Task AssignRoleAsync(Guid userId, Guid roleId)
        {
            var userRole = new UserRole
            {
                RoleId = roleId,
                UserId = userId
            };
            await _context.UserRoles.AddAsync(userRole);
        }
        public async Task UnassignRoleAsync(Guid userId, Guid roleId)
        {
            var userRole = await _context.UserRoles.Where(x => x.RoleId == roleId && x.UserId == userId).FirstOrDefaultAsync();
        }

        public async Task<bool> IsUserInRoleAsync(Guid userId, Guid roleId)
        {
            return await _context.UserRoles
                .AnyAsync(ur => ur.UserId == userId && ur.RoleId == roleId);
        }
        public async Task<IEnumerable<Guid>> GetRoleIdsAsync(Guid userId)
        {
            
            var ids = await _context.UserRoles.Where(x => x.UserId == userId).Select(x => x.RoleId).ToListAsync();
            return ids;
        } 
    }
}
