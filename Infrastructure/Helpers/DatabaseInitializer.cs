using Infrastructure.Data;
using Infrastructure.Seeds;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Helpers
{
    public static class DatabaseInitializer
    {
        public static async Task InitializeDatabaseAsync(this IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var services = scope.ServiceProvider;

            try
            {
                var context = services.GetRequiredService<Context>();

                await ContextSeed.SeedAsync(context);
            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Context>>();
                logger.LogError(ex, "An error occurred during DB initialization (Migration/Seed).");
                throw;
            }
        }
    }
}
