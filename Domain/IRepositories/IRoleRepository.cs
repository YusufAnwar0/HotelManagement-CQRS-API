using Domain.Models;

namespace Domain.IRepositories
{
    public interface IRoleRepository : IGeneralRepository<Role>
    {
        public Task<Guid> GetRoleIdByNameAsync(string roleName);
    }
}
