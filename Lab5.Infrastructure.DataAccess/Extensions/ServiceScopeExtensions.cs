using Itmo.Dev.Platform.Postgres.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Lab5.Infrastructure.DataAccess.Extensions;

public static class ServiceScopeExtensions
{
    public static async Task UseInfrastructureDataAccess(this IServiceScope scope)
    {
        await scope.UsePlatformMigrationsAsync(CancellationToken.None);
    }
}