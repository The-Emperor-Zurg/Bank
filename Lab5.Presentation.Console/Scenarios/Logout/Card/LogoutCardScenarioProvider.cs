using System.Diagnostics.CodeAnalysis;
using Lab5.Application.Cantracts.Users;

namespace Lab5.Presentation.Console.Scenarios.Logout;

public class LogoutCardScenarioProvider : IScenarioProvider
{
    private readonly IUserService _service;
    private readonly ICurrentUserService _currentUser;

    public LogoutCardScenarioProvider(
        IUserService service,
        ICurrentUserService currentUser)
    {
        _service = service;
        _currentUser = currentUser;
    }

    public bool TryGetScenario(
        [NotNullWhen(true)] out IScenario? scenario)
    {
        if (_currentUser.Card is null)
        {
            scenario = null;
            return false;
        }

        scenario = new LogoutCardScenario(_service);
        return true;
    }
}