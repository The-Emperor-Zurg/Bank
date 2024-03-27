using Itmo.Dev.Platform.Postgres.Connection;
using Itmo.Dev.Platform.Postgres.Extensions;
using Lab5.Application.Abstractions.Repositories;
using Lab5.Application.Models.People;
using Npgsql;

namespace Lab5.Infrastructure.DataAccess.Repositories;

public class UserRepository : IUserRepository
{
    private readonly IPostgresConnectionProvider _connectionProvider;
    public UserRepository(IPostgresConnectionProvider connectionProvider)
    {
        _connectionProvider = connectionProvider;
    }

    public async Task AddUser(string userName, string password)
    {
        const string sql =
            """
            Insert into Users(user_name, user_password)
            Values(@userName, @password)
            """;

        NpgsqlConnection connection = await _connectionProvider.GetConnectionAsync(CancellationToken.None);
        using var command = new NpgsqlCommand(sql, connection);
        command.AddParameter("userName", userName);
        command.AddParameter("password", password);

        await command.ExecuteNonQueryAsync();
    }

    public async Task<User?> FindUserByName(string userName)
    {
        const string sql =
            """
            select user_name, user_id, user_password
            from Users
            where user_name = @username
            """;

        NpgsqlConnection connection = await _connectionProvider.GetConnectionAsync(CancellationToken.None);
        using var command = new NpgsqlCommand(sql, connection);
        command.AddParameter("username", userName);

        using NpgsqlDataReader reader = await command.ExecuteReaderAsync();

        if (!await reader.ReadAsync())
        {
            return null;
        }

        return new User(reader.GetString(0), reader.GetInt64(1), reader.GetString(2));
    }
}