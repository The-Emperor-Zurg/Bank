using Lab5.Application.Cantracts.Users;
using Spectre.Console;

namespace Lab5.Presentation.Console.Scenarios.SetNewPassword;

public class SetNewPasswordScenario : IScenario
{
    private readonly IAdminService _adminService;

    public SetNewPasswordScenario(IAdminService userService)
    {
        _adminService = userService;
    }

    public string Name => "Set new password";

    public async Task Run()
    {
        string password = AnsiConsole.Ask<string>("Enter your new password");

        await _adminService.SetNewPassword(password);
        AnsiConsole.WriteLine("Successful!");
        AnsiConsole.Ask<string>("Ok");
    }
}