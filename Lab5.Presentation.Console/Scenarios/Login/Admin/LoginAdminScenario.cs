using Lab5.Application.Cantracts;
using Lab5.Application.Cantracts.Users;
using Spectre.Console;

namespace Lab5.Presentation.Console.Scenarios.Login;

public class LoginAdminScenario : IScenario
{
    private readonly IAdminService _adminService;

    public LoginAdminScenario(IAdminService userService)
    {
        _adminService = userService;
    }

    public string Name => "Login admin";

    public async Task Run()
    {
        string password = AnsiConsole.Ask<string>("Enter your password");

        Task<BaseResult> result = _adminService.LoginAdmin(password);

        string message = await result switch
        {
            BaseResult.Success => "Successful login",
            BaseResult.Unluck => "Incorrect password, session ended",
            _ => throw new ArgumentOutOfRangeException(nameof(result)),
        };

        AnsiConsole.WriteLine(message);
        if (message == "Incorrect password, session ended")
        {
            Environment.Exit(0);
        }

        AnsiConsole.Ask<string>("Ok");
    }
}