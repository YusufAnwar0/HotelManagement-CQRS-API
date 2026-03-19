using Application.Interface;
using Domain.Enums;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;

namespace Infrastructure.Service
{
    public class PermissionService : IPermissionService
    {
        private readonly Context _context;
        private readonly IDistributedCache _redisCache;
        private const string GlobalPermissionsCacheKey = "global_role_permissions";

        public PermissionService(Context context, IDistributedCache redisCache)
        {
            _context = context;
            _redisCache = redisCache;
        }

        public async Task<bool> HasPermissionAsync(Guid userId, List<Guid> roleIds, Permissions permission)
        {

            Dictionary<Guid, HashSet<Permissions>> allRolePermissions;

            var cachedData = await _redisCache.GetStringAsync(GlobalPermissionsCacheKey);

            if (!string.IsNullOrEmpty(cachedData))
            {
                allRolePermissions = JsonSerializer.Deserialize<Dictionary<Guid, HashSet<Permissions>>>(cachedData);
            }
            else
            {
                var rawData = await _context.RolePermissions
                        .Select(rp => new { rp.RoleId, rp.Permission })
                        .ToListAsync();

                allRolePermissions = rawData
                        .GroupBy(rp => rp.RoleId)
                        .ToDictionary(
                            group => group.Key,
                            group => group.Select(rp => rp.Permission).ToHashSet()
                        );

                var serializedData = JsonSerializer.Serialize(allRolePermissions);
                var cacheOptions = new DistributedCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromHours(1));

                await _redisCache.SetStringAsync(GlobalPermissionsCacheKey, serializedData, cacheOptions);

            }

            foreach (var roleId in roleIds)
            {
                if (allRolePermissions.TryGetValue(roleId, out var permissionsForThisRole))
                {
                    if (permissionsForThisRole.Contains(permission))
                    {
                        return true;
                    }
                }
            }

            return false;

        }
    }
}
