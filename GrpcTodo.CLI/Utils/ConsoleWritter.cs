namespace GrpcTodo.CLI.Utils;

public static class ConsoleWritter
{
    private static void Write(ConsoleColor color, string message)
    {
        Console.ForegroundColor = color;

        Console.WriteLine(message);

        Console.ResetColor();
    }

    public static void WriteError(string line)
    {
        Write(ConsoleColor.Red, $"\n[!] {line}\n");
    }

    public static void WriteSuccess(string line, string label = "+")
    {
        Write(ConsoleColor.Green, $"[{label}] {line}");
    }

    public static void WriteInfo(string line)
    {
        Write(ConsoleColor.Blue, line);
    }

    public static void Write(string line)
    {
        Write(ConsoleColor.White, line);
    }
}