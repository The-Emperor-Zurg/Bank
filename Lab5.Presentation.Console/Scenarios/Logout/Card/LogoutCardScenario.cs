using Lab5.Application.Cantracts.Users;
using Spectre.Console;

namespace Lab5.Presentation.Console.Scenarios.Logout;

public class LogoutCardScenario : IScenario
{
    private readonly IUserService _userService;

    public LogoutCardScenario(IUserService userService)
    {
        _userService = userService;
    }

    public string Name => "Logout to user";

    public async Task Run()
    {
        await _userService.LogoutCard();

        AnsiConsole.WriteLine("Successful logout to user page");
        AnsiConsole.Ask<string>("Ok");
    }
}