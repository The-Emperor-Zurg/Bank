using System.Diagnostics.CodeAnalysis;
using Lab5.Application.Cantracts.Users;

namespace Lab5.Presentation.Console.Scenarios.Login.User;

public class LoginUserScenarioProvider : IScenarioProvider
{
    private readonly IUserService _service;
    private readonly ICurrentUserService _currentUser;
    private readonly ICurrentAdminService _currentAdmin;

    public LoginUserScenarioProvider(
        IUserService service,
        ICurrentUserService currentUser,
        ICurrentAdminService currentAdmin)
    {
        _service = service;
        _currentUser = currentUser;
        _currentAdmin = currentAdmin;
    }

    public bool TryGetScenario(
        [NotNullWhen(true)] out IScenario? scenario)
    {
        if (_currentUser.User is null && _currentAdmin.Admin is null)
        {
            scenario = new LoginUserScenario(_service);
            return true;
        }

        scenario = null;
        return false;
    }
}