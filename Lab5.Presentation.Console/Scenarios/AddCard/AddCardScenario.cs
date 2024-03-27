using Lab5.Application.Cantracts.Users;
using Spectre.Console;

namespace Lab5.Presentation.Console.Scenarios.AddCard;

public class AddCardScenario : IScenario
{
    private readonly IUserService _userService;

    public AddCardScenario(IUserService userService)
    {
        _userService = userService;
    }

    public string Name => "Add a card to the user";

    public async Task Run()
    {
        string password = AnsiConsole.Ask<string>("Enter a password");

        await _userService.AddCard(password);

        AnsiConsole.WriteLine("Card is created!");
        AnsiConsole.Ask<string>("Ok");
    }
}