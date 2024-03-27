namespace Lab5.Application.Abstractions.Repositories;

public interface ITransactionRepository
{
    Task<string> ReceiveTransactions(long cardID);
    Task CreateNewTransaction(long cardId, decimal money);
}