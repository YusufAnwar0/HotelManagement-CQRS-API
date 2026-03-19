using Domain.Enums;

namespace Application.Interface
{
    public interface IPermissionService
    {
        Task<bool> HasPermissionAsync(Guid userId, List<Guid> roleIds, Permissions permission);
    }
}
