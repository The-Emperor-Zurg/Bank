using Lab5.Application.Abstractions.Repositories;
using Lab5.Application.Cantracts;
using Lab5.Application.Cantracts.Users;
using Lab5.Application.Cantracts.Users.Results;
using Lab5.Application.Models;
using Lab5.Application.Models.People;

namespace Lab5.Application;

public class UserService : IUserService
{
    private readonly IUserRepository _repository;
    private readonly ICardRepository _cardRepository;
    private readonly ITransactionRepository _transactionRepository;
    private CurrentUserManager _currentUserManager;

    public UserService(
        CurrentUserManager currentUserManager,
        IUserRepository repository,
        ICardRepository cardRepository,
        ITransactionRepository transactionRepository)
    {
        _currentUserManager = currentUserManager;
        _repository = repository;
        _cardRepository = cardRepository;
        _transactionRepository = transactionRepository;
    }

    public async Task<LoginUserResult> LoginUser(string userName, string password)
    {
        Task<User?> user = _repository.FindUserByName(userName);

        if (await user is null)
        {
            return LoginUserResult.NoUser;
        }

        if ((await user)?.Password != password)
        {
            return LoginUserResult.NoPassword;
        }

        _currentUserManager.User = await user;

        return LoginUserResult.Success;
    }

    public async Task<LoginCardResult> LoginCard(long cardID, string pinCode)
    {
        Task<Card?> card = _cardRepository.FindCardByID(cardID);

        if (await card is null)
        {
            return LoginCardResult.NoCard;
        }

        if ((await card)?.PinCode != pinCode)
        {
            return LoginCardResult.NoPincode;
        }

        _currentUserManager.Card = await card;

        return LoginCardResult.Succeess;
    }

    public Task LogoutUser()
    {
        _currentUserManager.User = null;

        return Task.CompletedTask;
    }

    public Task LogoutCard()
    {
        _currentUserManager.Card = null;

        return Task.CompletedTask;
    }

    public async Task AddCard(string pinCode)
    {
        if (_currentUserManager.User is null)
        {
            throw new NoUserException("No User!");
        }

        await _cardRepository.CreateNewCard(_currentUserManager.User.ID, pinCode);
    }

    public async Task<BaseResult> PutMoneyInto(decimal money)
    {
        if (_currentUserManager.Card is null)
        {
            throw new NoCardException("No card!");
        }

        if (money <= 0)
        {
            return BaseResult.Unluck;
        }

        await _cardRepository.PutMoney(_currentUserManager.Card.ID, money);
        await _transactionRepository.CreateNewTransaction(_currentUserManager.Card.ID, money);
        _currentUserManager.Card.ChangeBalace(money);

        return BaseResult.Success;
    }

    public Task<decimal> ShowBalance()
    {
        if (_currentUserManager.Card is null)
        {
            throw new NoCardException("No card!");
        }

        return Task.FromResult(_currentUserManager.Card.Balance);
    }

    public async Task<string> ShowHistory()
    {
        if (_currentUserManager.Card is null)
        {
            throw new NoCardException("No card!");
        }

        return await _transactionRepository.ReceiveTransactions(_currentUserManager.Card.ID);
    }

    public async Task<BaseResult> WithDrawMoney(decimal money)
    {
        if (_currentUserManager.Card is null)
        {
            throw new NoCardException("No card!");
        }

        if (money <= 0 || _currentUserManager.Card.Balance < money)
        {
            return BaseResult.Unluck;
        }

        await _cardRepository.WithdrawMoney(_currentUserManager.Card.ID, money);
        await _transactionRepository.CreateNewTransaction(_currentUserManager.Card.ID, -money);

        return BaseResult.Success;
    }
}