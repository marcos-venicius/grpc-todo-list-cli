using System.Text;

namespace GrpcTodo.CLI.Utils;

internal static class ConsoleReader
{
    public static string ReadLine(bool shouldBeHidden = false, string? hiddenSymbol = null)
    {
        var data = shouldBeHidden ? ReadHiddenLine(hiddenSymbol) : Console.ReadLine();

        data ??= "";

        return data;
    }

    private static string ReadHiddenLine(string? hiddenSymbol)
    {
        hiddenSymbol ??= string.Empty;

        StringBuilder data = new();

        ConsoleKey key;

        do
        {
            var keyInfo = Console.ReadKey(intercept: true);

            key = keyInfo.Key;

            if (key == ConsoleKey.Backspace && data.Length > 0)
            {
                if (hiddenSymbol != string.Empty)
                    Console.Write("\b \b");

                data.Remove(data.Length - 1, 1);
            }
            else if (!char.IsControl(keyInfo.KeyChar))
            {
                if (hiddenSymbol != string.Empty)
                    Console.Write(hiddenSymbol);

                data.Append(keyInfo.KeyChar);
            }
        } while (key != ConsoleKey.Enter);

        Console.WriteLine();

        return data.ToString();
    }
}
