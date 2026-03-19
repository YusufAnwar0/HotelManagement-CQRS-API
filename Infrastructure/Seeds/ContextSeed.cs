using Domain.Constants;
using Domain.Enums;
using Domain.Models;
using Infrastructure.Authentication;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Seeds
{
    public class ContextSeed
    {
        public static async Task SeedAsync(Context context)
        {
            using var transaction = await context.Database.BeginTransactionAsync();
            try
            {
                if (context.Roles.Any()) return;

                var roles = new List<Role>
                {
                    new Role { Id = RoleConstants.AdminRoleId, Name = Roles.ADMIN.ToString() },
                    new Role { Id = RoleConstants.CustomerRoleId, Name = Roles.CUSTOMER.ToString() },
                };

                await context.Roles.AddRangeAsync(roles);

                if (!await context.Users.AnyAsync(u => u.Id == RoleConstants.AdminUserId))
                {
                    var passwordHasher = new PasswordHasher();

                    var adminUser = new User
                    {
                        Id = RoleConstants.AdminUserId,
                        UserName = "admin",
                        Email = "admin@hotel.com",
                        PasswordHash = passwordHasher.Hash("admin123"),
                        PhoneNumber = "",
                        Country = "Egypt"
                    };

                    await context.Users.AddAsync(adminUser);

                    var userRole = new UserRole
                    {
                        UserId = RoleConstants.AdminUserId,
                        RoleId = RoleConstants.AdminRoleId
                    };

                    await context.UserRoles.AddAsync(userRole);
                }
                await context.SaveChangesAsync();
                await transaction.CommitAsync();
            }
            catch(Exception) 
            {
                await transaction.RollbackAsync();
                throw;
            }

        }
    }
}
