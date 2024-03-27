using Lab5.Application.Cantracts;
using Lab5.Application.Cantracts.Users;
using Spectre.Console;

namespace Lab5.Presentation.Console.Scenarios.PutMoneyIntoAccount;

public class PutMoneyIntoScenario : IScenario
{
    private readonly IUserService _userService;

    public PutMoneyIntoScenario(IUserService userService)
    {
        _userService = userService;
    }

    public string Name => "Put money into the account";

    public async Task Run()
    {
        decimal money = AnsiConsole.Ask<decimal>("How much money do you want to put into?");

        Task<BaseResult> result = _userService.PutMoneyInto(money);

        string message = await result switch
        {
            BaseResult.Success => "Successful put money into the account ",
            BaseResult.Unluck => "Negative amount - it's not possible",
            _ => throw new ArgumentOutOfRangeException(nameof(result)),
        };

        AnsiConsole.WriteLine(message);
        AnsiConsole.Ask<string>("Ok");
    }
}