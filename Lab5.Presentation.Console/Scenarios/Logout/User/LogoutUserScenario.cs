using Lab5.Application.Cantracts.Users;
using Spectre.Console;

namespace Lab5.Presentation.Console.Scenarios.Logout;

public class LogoutUserScenario : IScenario
{
    private readonly IUserService _userService;

    public LogoutUserScenario(IUserService userService)
    {
        _userService = userService;
    }

    public string Name => "Logout to Home Page";

    public async Task Run()
    {
        await _userService.LogoutUser();

        AnsiConsole.WriteLine("Successful logout to home page");
        AnsiConsole.Ask<string>("Ok");
    }
}