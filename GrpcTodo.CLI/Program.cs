using GrpcTodo.CLI;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var cli = new CLI(args);

        await cli.Run();
    }
}