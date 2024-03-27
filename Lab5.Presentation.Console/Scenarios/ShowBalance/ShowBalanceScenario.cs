using Lab5.Application.Cantracts.Users;
using Spectre.Console;

namespace Lab5.Presentation.Console.Scenarios.ShowBalance;

public class ShowBalanceScenario : IScenario
{
    private readonly IUserService _userService;

    public ShowBalanceScenario(IUserService userService)
    {
        _userService = userService;
    }

    public string Name => "Show balance";

    public async Task Run()
    {
        decimal balance = await _userService.ShowBalance();

        AnsiConsole.WriteLine($"Your current balance is {balance}");
        AnsiConsole.Ask<string>("Ok");
    }
}