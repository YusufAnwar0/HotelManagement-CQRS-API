using Domain.Models;

namespace Domain.IRepositories
{
    public interface IUserRepository : IGeneralRepository<User>
    {
        public Task AssignRoleAsync(Guid userId, Guid roleId);
        Task<bool> IsUserInRoleAsync(Guid userId, Guid roleId);
        public Task<IEnumerable<Guid>> GetRoleIdsAsync(Guid userId);
    }
}
