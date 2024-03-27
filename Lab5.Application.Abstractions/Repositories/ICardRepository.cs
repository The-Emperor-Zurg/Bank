using Lab5.Application.Models;

namespace Lab5.Application.Abstractions.Repositories;

public interface ICardRepository
{
    Task<Card?> FindCardByID(long id);
    Task PutMoney(long cardID, decimal money);
    Task CreateNewCard(long userID, string pincode);
    Task WithdrawMoney(long cardID, decimal money);
}