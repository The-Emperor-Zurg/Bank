using System.Diagnostics.CodeAnalysis;
using Lab5.Application.Cantracts.Users;

namespace Lab5.Presentation.Console.Scenarios.Login;

public class LoginAdminScenarioProvider : IScenarioProvider
{
    private readonly IAdminService _service;
    private readonly ICurrentAdminService _currentAdmin;
    private readonly ICurrentUserService _currentUser;

    public LoginAdminScenarioProvider(
        IAdminService service,
        ICurrentAdminService currentAdmin,
        ICurrentUserService currentUser)
    {
        _service = service;
        _currentAdmin = currentAdmin;
        _currentUser = currentUser;
    }

    public bool TryGetScenario(
        [NotNullWhen(true)] out IScenario? scenario)
    {
        if (_currentAdmin.Admin is null && _currentUser.User is null)
        {
            scenario = new LoginAdminScenario(_service);
            return true;
        }

        scenario = null;
        return false;
    }
}