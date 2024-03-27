using Lab5.Application.Cantracts.Users.Results;

namespace Lab5.Application.Cantracts.Users;

public interface IUserService
{
    Task<LoginUserResult> LoginUser(string userName, string password);
    Task AddCard(string pinCode);
    Task<LoginCardResult> LoginCard(long cardID, string pinCode);
    Task LogoutUser();
    Task LogoutCard();
    Task<BaseResult> PutMoneyInto(decimal money);
    Task<decimal> ShowBalance();
    Task<string> ShowHistory();
    Task<BaseResult> WithDrawMoney(decimal money);
}