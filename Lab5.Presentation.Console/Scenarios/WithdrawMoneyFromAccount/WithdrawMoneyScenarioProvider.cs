using System.Diagnostics.CodeAnalysis;
using Lab5.Application.Cantracts.Users;

namespace Lab5.Presentation.Console.Scenarios.WithdrawMoneyFromAccount;

public class WithdrawMoneyScenarioProvider : IScenarioProvider
{
    private readonly IUserService _service;
    private readonly ICurrentUserService _currentUser;

    public WithdrawMoneyScenarioProvider(
        IUserService service,
        ICurrentUserService currentUser)
    {
        _service = service;
        _currentUser = currentUser;
    }

    public bool TryGetScenario(
        [NotNullWhen(true)] out IScenario? scenario)
    {
        if (_currentUser.Card is not null)
        {
            scenario = new WithdrawMoneyScenario(_service);
            return true;
        }

        scenario = null;
        return false;
    }
}