namespace GrpcTodo.CLI.Utils;

public static class ConsoleWritter
{
    public static void WriteLine(string line, bool? clear = false)
    {
        if (clear == true)
            Console.Clear();

        Console.WriteLine($"\n{line}\n");
    }
}