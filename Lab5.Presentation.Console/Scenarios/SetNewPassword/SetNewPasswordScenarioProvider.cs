using System.Diagnostics.CodeAnalysis;
using Lab5.Application.Cantracts.Users;
using Lab5.Presentation.Console.Scenarios.SetNewPassword;

namespace Lab5.Presentation.Console.Scenarios;

public class SetNewPasswordScenarioProvider : IScenarioProvider
{
    private readonly IAdminService _service;
    private readonly ICurrentAdminService _currentAdmin;

    public SetNewPasswordScenarioProvider(
        IAdminService service,
        ICurrentAdminService currentAdmin)
    {
        _service = service;
        _currentAdmin = currentAdmin;
    }

    public bool TryGetScenario(
        [NotNullWhen(true)] out IScenario? scenario)
    {
        if (_currentAdmin.Admin is null)
        {
            scenario = null;
            return false;
        }

        scenario = new SetNewPasswordScenario(_service);
        return true;
    }
}