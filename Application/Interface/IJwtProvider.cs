using Domain.Models;

namespace Application.Interface
{
    public interface IJwtProvider
    {
        string GenerateToken(Guid userId, IEnumerable<Guid> roles);
    }
}
