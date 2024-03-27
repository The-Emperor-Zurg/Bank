using Itmo.Dev.Platform.Postgres.Connection;
using Itmo.Dev.Platform.Postgres.Extensions;
using Lab5.Application.Abstractions.Repositories;
using Lab5.Application.Models;
using Npgsql;

namespace Lab5.Infrastructure.DataAccess.Repositories;

public class CardRepository : ICardRepository
{
    private readonly IPostgresConnectionProvider _connectionProvider;
    public CardRepository(IPostgresConnectionProvider connectionProvider)
    {
        _connectionProvider = connectionProvider;
    }

    public async Task<Card?> FindCardByID(long id)
    {
        const string sql =
            """
            select user_id, card_id, pin_code, balance
            from Cards
            where card_id = @id
            """;

        NpgsqlConnection connection = await _connectionProvider.GetConnectionAsync(CancellationToken.None);
        using var command = new NpgsqlCommand(sql, connection);
        command.AddParameter("id", id);

        using NpgsqlDataReader reader = await command.ExecuteReaderAsync();

        if (!await reader.ReadAsync())
        {
            return null;
        }

        return new Card(reader.GetInt64(0), reader.GetInt64(1), reader.GetString(2), reader.GetDecimal(3));
    }

    public async Task PutMoney(long cardID, decimal money)
    {
        const string sql =
            """
            UPDATE Cards
            SET balance = balance + @money
            WHERE card_id = @cardID
            """;

        NpgsqlConnection connection = await _connectionProvider.GetConnectionAsync(CancellationToken.None);
        using var command = new NpgsqlCommand(sql, connection);
        command.AddParameter("money", money);
        command.AddParameter("cardID", cardID);

        await command.ExecuteNonQueryAsync();
    }

    public async Task CreateNewCard(long userID, string pincode)
    {
        const string sql =
            """
            Insert into Cards(user_id, pin_code, balance)
            Values(@user_Id, @pin_code, 0)
            """;

        NpgsqlConnection connection = await _connectionProvider.GetConnectionAsync(CancellationToken.None);
        using var command = new NpgsqlCommand(sql, connection);
        command.AddParameter("user_id", userID);
        command.AddParameter("pin_code", pincode);

        await command.ExecuteNonQueryAsync();
    }

    public async Task WithdrawMoney(long cardID, decimal money)
    {
        const string sql =
            """
            UPDATE Cards
            SET balance = balance - @money
            WHERE card_id = @cardID
            """;

        NpgsqlConnection connection = await _connectionProvider.GetConnectionAsync(CancellationToken.None);
        using var command = new NpgsqlCommand(sql, connection);
        command.AddParameter("money", money);
        command.AddParameter("cardID", cardID);

        using NpgsqlDataReader reader = await command.ExecuteReaderAsync();
        if (!await reader.ReadAsync())
        {
            return;
        }

        await command.ExecuteNonQueryAsync();
    }
}