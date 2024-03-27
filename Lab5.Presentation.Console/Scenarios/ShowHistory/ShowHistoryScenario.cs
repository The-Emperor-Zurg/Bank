using Lab5.Application.Cantracts.Users;
using Spectre.Console;

namespace Lab5.Presentation.Console.Scenarios.ShowHistory;

public class ShowHistoryScenario : IScenario
{
    private readonly IUserService _userService;

    public ShowHistoryScenario(IUserService userService)
    {
        _userService = userService;
    }

    public string Name => "Show history";

    public async Task Run()
    {
        string transactions = await _userService.ShowHistory();

        if (string.IsNullOrEmpty(transactions))
        {
            AnsiConsole.WriteLine("You have no operations with this card!");
            AnsiConsole.Ask<string>("Ok");
            return;
        }

        AnsiConsole.WriteLine(transactions);
        AnsiConsole.WriteLine("It is your card history");
        AnsiConsole.Ask<string>("Ok");
    }
}