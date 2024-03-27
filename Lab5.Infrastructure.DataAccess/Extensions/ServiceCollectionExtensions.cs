using Itmo.Dev.Platform.Postgres.Extensions;
using Itmo.Dev.Platform.Postgres.Models;
using Lab5.Application;
using Lab5.Application.Abstractions.Repositories;
using Lab5.Infrastructure.DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Lab5.Infrastructure.DataAccess.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureDataAccess(
        this IServiceCollection collection,
        Action<PostgresConnectionConfiguration> configuration)
    {
        if (configuration == null) throw new ArgumentNullException(nameof(configuration));
        collection.AddPlatformPostgres(builder => builder.Configure(configuration));
        collection.AddPlatformMigrations(typeof(ServiceCollectionExtensions).Assembly);

        collection.AddScoped<IUserRepository, UserRepository>();
        collection.AddScoped<ICardRepository, CardRepository>();
        collection.AddScoped<ITransactionRepository, TransactionRepository>();

        return collection;
    }
}