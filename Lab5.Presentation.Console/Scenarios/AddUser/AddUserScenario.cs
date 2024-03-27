using Lab5.Application.Cantracts;
using Lab5.Application.Cantracts.Users;
using Spectre.Console;

namespace Lab5.Presentation.Console.Scenarios.AddCustomer;

public class AddUserScenario : IScenario
{
    private readonly IAdminService _adminService;

    public AddUserScenario(IAdminService adminService)
    {
        _adminService = adminService;
    }

    public string Name => "Add a user";

    public async Task Run()
    {
        string userName = AnsiConsole.Ask<string>("Enter your name");
        string password = AnsiConsole.Ask<string>("Enter your new password");

        Task<BaseResult> result = _adminService.AddUser(userName, password);

        string message = await result switch
        {
            BaseResult.Success => "Successful add new user",
            BaseResult.Unluck => "This user exists",
            _=> throw new ArgumentOutOfRangeException(nameof(result)),
        };

        AnsiConsole.WriteLine(message);
        AnsiConsole.Ask<string>("Ok");
    }
}