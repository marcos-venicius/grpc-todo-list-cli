using GrpcTodo.CLI;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var cli = new CLI(args);

        cli.ReadArgs();

        if (!cli.HasPossibleCommands())
        {
            cli.Menu.ShowAvailableOptions();

            return;
        }

        await cli.Run();
    }
}