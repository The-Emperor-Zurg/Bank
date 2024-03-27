using Lab5.Application.Cantracts.Users;
using Spectre.Console;

namespace Lab5.Presentation.Console.Scenarios.Logout;

public class LogoutAdminScenario : IScenario
{
    private readonly IAdminService _adminService;

    public LogoutAdminScenario(IAdminService userService)
    {
        _adminService = userService;
    }

    public string Name => "Logout to home page";

    public async Task Run()
    {
        await _adminService.Logout();
        AnsiConsole.WriteLine("Successful!");
        AnsiConsole.Ask<string>("Ok");
    }
}