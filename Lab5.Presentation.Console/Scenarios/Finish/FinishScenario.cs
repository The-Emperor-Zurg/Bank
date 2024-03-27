namespace Lab5.Presentation.Console.Scenarios.Finish;

public class FinishScenario : IScenario
{
    public string Name => "Finish";

    public Task Run()
    {
        System.Console.WriteLine("Thank you for using our bank's services!\nHave a nice day!");
        Environment.Exit(0);

        return Task.CompletedTask;
    }
}