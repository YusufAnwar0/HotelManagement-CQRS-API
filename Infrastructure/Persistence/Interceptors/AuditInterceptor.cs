using Application.Interface;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infrastructure.Persistence.Interceptors
{
    public class AuditInterceptor : SaveChangesInterceptor
    {
        private readonly ICurrentUserService _currentUserService;
        public AuditInterceptor(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            var dbContext = eventData.Context;

            if(dbContext == null)
                return base.SavingChangesAsync(eventData, result, cancellationToken);

            var entries = dbContext.ChangeTracker.Entries<BaseModel>();

            var now = DateTime.UtcNow;
            var userId = _currentUserService.CurrentUserId;

            foreach (var entry in entries)
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = now;
                    entry.Entity.CreatedBy = userId;
                }
                else if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedAt = now;
                    entry.Entity.UpdatedBy = userId;
                }
                else if (entry.State == EntityState.Deleted)
                {
                    entry.State = EntityState.Unchanged;

                    entry.Property(x => x.IsDeleted).CurrentValue = true;
                    entry.Property(x => x.IsDeleted).IsModified = true;

                    entry.Property(x => x.UpdatedAt).CurrentValue = now;
                    entry.Property(x => x.UpdatedAt).IsModified = true;

                    entry.Property(x => x.UpdatedBy).CurrentValue = userId;
                    entry.Property(x => x.UpdatedBy).IsModified = true;

                }
            }

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

    }
}
