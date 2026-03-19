using Application.Interface;
using Domain.Constants;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace Presentation.Filters
{
    public class PermissionAuthorizationFilter : IAsyncAuthorizationFilter
    {
        private readonly Permissions _permission;
        private readonly IPermissionService _permissionService;

        public PermissionAuthorizationFilter(Permissions permission, IPermissionService permissionService)
        {
            _permission = permission;
            _permissionService = permissionService;
        }
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            var userIdClaim = context.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(userIdClaim, out Guid userId))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var roleClaims = context.HttpContext.User.FindAll(ClaimTypes.Role);

            var roleIds = roleClaims.Select(c => Guid.Parse(c.Value)).ToList();

            if (roleIds.Contains(RoleConstants.AdminRoleId))
                return;

            var hasPermission = await _permissionService.HasPermissionAsync(userId, roleIds, _permission);

            if (hasPermission)
                return;

            context.Result = new ForbidResult();

        }
    }
}
