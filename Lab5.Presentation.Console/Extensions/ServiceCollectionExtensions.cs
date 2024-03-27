using Lab5.Presentation.Console.Scenarios;
using Lab5.Presentation.Console.Scenarios.AddCard;
using Lab5.Presentation.Console.Scenarios.AddCustomer;
using Lab5.Presentation.Console.Scenarios.Finish;
using Lab5.Presentation.Console.Scenarios.Login;
using Lab5.Presentation.Console.Scenarios.Login.User;
using Lab5.Presentation.Console.Scenarios.Logout;
using Lab5.Presentation.Console.Scenarios.Logout.Admin;
using Lab5.Presentation.Console.Scenarios.PutMoneyIntoAccount;
using Lab5.Presentation.Console.Scenarios.ShowBalance;
using Lab5.Presentation.Console.Scenarios.ShowHistory;
using Lab5.Presentation.Console.Scenarios.WithdrawMoneyFromAccount;
using Microsoft.Extensions.DependencyInjection;

namespace Lab5.Presentation.Console.Extenctions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPresentationConsole(this IServiceCollection collection)
    {
        collection.AddScoped<ScenarioRunner>();

        collection.AddScoped<IScenarioProvider, LoginAdminScenarioProvider>();
        collection.AddScoped<IScenarioProvider, LoginUserScenarioProvider>();
        collection.AddScoped<IScenarioProvider, AddUserScenarioProvider>();
        collection.AddScoped<IScenarioProvider, LoginCardScenarioProvider>();
        collection.AddScoped<IScenarioProvider, AddCardScenarioProvider>();
        collection.AddScoped<IScenarioProvider, LogoutAdminScenarioProvider>();
        collection.AddScoped<IScenarioProvider, LogoutCardScenarioProvider>();
        collection.AddScoped<IScenarioProvider, LogoutUserScenarioProvider>();
        collection.AddScoped<IScenarioProvider, PutMoneyIntoScenarioProvider>();
        collection.AddScoped<IScenarioProvider, ShowHistoryScenarioProvider>();
        collection.AddScoped<IScenarioProvider, WithdrawMoneyScenarioProvider>();
        collection.AddScoped<IScenarioProvider, ShowBalanceScenarioProvider>();
        collection.AddScoped<IScenarioProvider, SetNewPasswordScenarioProvider>();
        collection.AddScoped<IScenarioProvider, FinishScenarioProvider>();

        return collection;
    }
}