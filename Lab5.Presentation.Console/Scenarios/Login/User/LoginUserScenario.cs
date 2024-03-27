using Lab5.Application.Cantracts;
using Lab5.Application.Cantracts.Users;
using Spectre.Console;

namespace Lab5.Presentation.Console.Scenarios.Login.User;

public class LoginUserScenario : IScenario
{
    private readonly IUserService _userService;

    public LoginUserScenario(IUserService userService)
    {
        _userService = userService;
    }

    public string Name => "Login user";

    public async Task Run()
    {
        string user = AnsiConsole.Ask<string>("Enter your name");
        string password = AnsiConsole.Ask<string>("Enter your password");

        Task<LoginUserResult> result = _userService.LoginUser(user, password);

        string message = await result switch
        {
            LoginUserResult.Success => "Successful login",
            LoginUserResult.NoUser => "There is no user like it",
            LoginUserResult.NoPassword => "Incorrect password",
            _ => throw new ArgumentOutOfRangeException(nameof(result)),
        };

        AnsiConsole.WriteLine(message);
        AnsiConsole.Ask<string>("Ok");
    }
}