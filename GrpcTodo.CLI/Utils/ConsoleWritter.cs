namespace GrpcTodo.CLI.Utils;

public static class ConsoleWritter
{
    private static void Write(ConsoleColor color, string message, bool breakLine = true)
    {
        Console.ForegroundColor = color;

        if (breakLine)
            Console.WriteLine(message);
        else
            Console.Write(message);

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

    public static void WriteInfo(string line, bool notBreakLine = false)
    {
        Write(ConsoleColor.Blue, line, notBreakLine);
    }

    public static void Write(string line, bool notBreakLine = false)
    {
        Write(ConsoleColor.White, line, notBreakLine);
    }
}