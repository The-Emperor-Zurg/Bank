using Lab5.Application.Cantracts.Users;
using Lab5.Application.Cantracts.Users.Results;
using Spectre.Console;

namespace Lab5.Presentation.Console.Scenarios.Login;

public class LoginCardScenario : IScenario
{
    private readonly IUserService _userService;

    public LoginCardScenario(IUserService userService)
    {
        _userService = userService;
    }

    public string Name => "Login card";

    public async Task Run()
    {
        long cardID = AnsiConsole.Ask<long>("Enter you cardID");
        string pin = AnsiConsole.Ask<string>("Enter your pin-code");

        Task<LoginCardResult> result = _userService.LoginCard(cardID, pin);

        string message = await result switch
        {
            LoginCardResult.Succeess => "Successful login",
            LoginCardResult.NoCard => "There is no card like this",
            LoginCardResult.NoPincode => "Incorrect pin-code ",
            _ => throw new ArgumentOutOfRangeException(nameof(result)),
        };

        AnsiConsole.WriteLine(message);
        AnsiConsole.Ask<string>("Ok");
    }
}