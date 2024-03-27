using Lab5.Application.Cantracts;
using Lab5.Application.Cantracts.Users;
using Spectre.Console;

namespace Lab5.Presentation.Console.Scenarios.WithdrawMoneyFromAccount;

public class WithdrawMoneyScenario : IScenario
{
    private readonly IUserService _userService;

    public WithdrawMoneyScenario(IUserService userService)
    {
        _userService = userService;
    }

    public string Name => "With draw money";

    public async Task Run()
    {
        decimal money = AnsiConsole.Ask<decimal>("How much money do you want to with draw?");

        Task<BaseResult> result = _userService.WithDrawMoney(money);

        string message = await result switch
        {
            BaseResult.Success => "Successful withdraw",
            BaseResult.Unluck => "You haven't got enough money",
            _ => throw new ArgumentOutOfRangeException(nameof(result)),
        };

        AnsiConsole.WriteLine(message);
        AnsiConsole.Ask<string>("Ok");
    }
}