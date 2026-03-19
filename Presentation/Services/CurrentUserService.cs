using Application.Interface;
using System.Security.Claims;

namespace Presentation.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public Guid? CurrentUserId
        {
            get
            {
                var userIdClaim = _httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);

                if (Guid.TryParse(userIdClaim, out Guid parsedUserId))
                {
                    return parsedUserId;
                }

                return null;
            }
        }
    }
}
