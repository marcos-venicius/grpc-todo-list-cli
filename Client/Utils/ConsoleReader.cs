namespace Client.Utils;

internal static class ConsoleReader
{
    public static string ReadString(string input)
    {
        Console.Write(input);

        var data = Console.ReadLine();

        data ??= "";

        return data;
    }
}