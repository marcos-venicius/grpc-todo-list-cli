using GrpcTodo.CLI;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var cli = new CLI(args);

        if (args.Length == 0)
        {
            cli.Menu.ShowAvailableOptions();

            return;
        }

        await cli.Run();
    }
}