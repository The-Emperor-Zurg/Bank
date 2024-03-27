using System.Diagnostics.CodeAnalysis;
using Lab5.Application.Cantracts.Users;

namespace Lab5.Presentation.Console.Scenarios.AddCustomer;

public class AddUserScenarioProvider : IScenarioProvider
{
    private readonly IAdminService _service;
    private readonly ICurrentAdminService _currentAdmin;

    public AddUserScenarioProvider(
        IAdminService service,
        ICurrentAdminService currentUser)
    {
        _service = service;
        _currentAdmin = currentUser;
    }

    public bool TryGetScenario(
        [NotNullWhen(true)] out IScenario? scenario)
    {
        if (_currentAdmin.Admin is null)
        {
            scenario = null;
            return false;
        }

        scenario = new AddUserScenario(_service);
        return true;
    }
}