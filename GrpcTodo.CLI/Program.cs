using GrpcTodo.CLI;


public static class Program
{
    public static void Main(string[] args)
    {
        var cli = new CLI(args);

        if (args.Length == 0)
            return;

        cli.Run();
    }
}