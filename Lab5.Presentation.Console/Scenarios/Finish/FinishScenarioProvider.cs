using System.Diagnostics.CodeAnalysis;
using Lab5.Application.Cantracts.Users;

namespace Lab5.Presentation.Console.Scenarios.Finish;

public class FinishScenarioProvider : IScenarioProvider
{
    private readonly ICurrentUserService _currentUser;
    private readonly ICurrentAdminService _currentAdmin;

    public FinishScenarioProvider(
        ICurrentUserService currentUser,
        ICurrentAdminService adminService)
    {
        _currentUser = currentUser;
        _currentAdmin = adminService;
    }

    public bool TryGetScenario(
        [NotNullWhen(true)] out IScenario? scenario)
    {
        if (_currentUser.User is null && _currentAdmin.Admin is null)
        {
            scenario = new FinishScenario();
            return true;
        }

        scenario = null;
        return false;
    }
}