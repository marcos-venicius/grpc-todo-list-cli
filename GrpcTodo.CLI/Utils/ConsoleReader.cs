namespace GrpcTodo.CLI.Utils;

internal static class ConsoleReader
{
    public static string ReadString(string input)
    {
        Console.Write(input);

        var data = Console.ReadLine();

        data ??= "";

        return data;
    }

    public static string? ReadLine(bool shouldBeHidden, string? hiddenSymbol = null)
    {
        return shouldBeHidden ? ReadHiddenLine(hiddenSymbol) : Console.ReadLine();
    }

    private static string ReadHiddenLine(string? hiddenSymbol)
    {
        hiddenSymbol ??= string.Empty;
        var data = string.Empty;
        ConsoleKey key;
        do
        {
            var keyInfo = Console.ReadKey(intercept: true);
            key = keyInfo.Key;

            if (key == ConsoleKey.Backspace && data.Length > 0)
            {
                if(hiddenSymbol != string.Empty)
                    Console.Write("\b \b");
                data = data[0..^1];
            }
            else if (!char.IsControl(keyInfo.KeyChar))
            {
                Console.Write(hiddenSymbol);
                data += keyInfo.KeyChar;
            }
        } while (key != ConsoleKey.Enter);

        return data;
    }
}