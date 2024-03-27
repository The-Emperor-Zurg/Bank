using System.Globalization;
using System.Text;
using Itmo.Dev.Platform.Postgres.Connection;
using Itmo.Dev.Platform.Postgres.Extensions;
using Lab5.Application.Abstractions.Repositories;
using Npgsql;

namespace Lab5.Application;

public class TransactionRepository : ITransactionRepository
{
    private readonly IPostgresConnectionProvider _connectionProvider;

    public TransactionRepository(IPostgresConnectionProvider connectionProvider)
    {
        _connectionProvider = connectionProvider;
    }

    public async Task<string> ReceiveTransactions(long cardID)
    {
        const string sql =
            """
            select card_id, money, date
            from Transactions
            where card_id = @cardID
            """;

        NpgsqlConnection connection = await _connectionProvider.GetConnectionAsync(CancellationToken.None);
        using var command = new NpgsqlCommand(sql, connection);
        command.AddParameter("cardID", cardID);

        using NpgsqlDataReader reader = await command.ExecuteReaderAsync();

        var transactions = new StringBuilder();
        while (await reader.ReadAsync())
        {
            transactions.AppendLine(
                CultureInfo.CurrentCulture,
                $"Operation: {reader.GetDecimal(1)}, Date: {reader.GetDateTime(2)}");
        }

        return transactions.ToString();
    }

    public async Task CreateNewTransaction(long cardId, decimal money)
    {
        const string sql = """
                           Insert into Transactions(card_id, money, date)
                           VALUES (@card_id, @money, @date)
                           """;

        NpgsqlConnection connection = await _connectionProvider.GetConnectionAsync(CancellationToken.None);
        using var command = new NpgsqlCommand(sql, connection);
        command.AddParameter("card_id", cardId);
        command.AddParameter("money", money);
        command.AddParameter("date", DateTime.Now);

        await command.ExecuteNonQueryAsync();
    }
}