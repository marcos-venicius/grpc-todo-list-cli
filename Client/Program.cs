using Client.Enums;
using Client.Models;
using Client.Utils;

public static class Program
{
    private static Dictionary<ushort, Option> Options = new()
    {
        { 1, new () { Action = ActionType.CreateTodo, Name = "create todo" } },
        { 2, new () { Action = ActionType.ListTodo, Name = "list todo" } },
        { 3, new () { Action = ActionType.Exit, Name = "exit" } }
    };

    private static Dictionary<string, ushort> Aliases = new()
    {
        { "ct", 1 },
        { "lt", 2 }
    };

    private static void ShowOptions()
    {
        Console.WriteLine("=== MENU");
        Console.WriteLine();

        foreach (var option in Options)
        {
            Console.WriteLine($"[{option.Key}] {option.Value.Name}");
        }
    }

    private static ActionType ReadOption()
    {
        Console.Write("> ");

        var readed = Console.ReadLine();

        if (!ushort.TryParse(readed, out var key))
            throw new InvalidOptionException();

        if (!Options.ContainsKey(key))
            throw new InvalidOptionException();

        return Options[key].Action;
    }

    private static ActionType IndentifyAlias(string alias)
    {
        if (Aliases.ContainsKey(alias))
            return Options[Aliases[alias]].Action;

        return ActionType.None;
    }

    public static void Main(string[] args)
    {
        while (true)
        {
            Console.Clear();

            try
            {
                if (args.Length >= 1)
                {
                    var alias = args[0];

                    var action = IndentifyAlias(alias);

                    if (action is not ActionType.None)
                    {
                        ActionRunner.Run(action);
                        break;
                    }
                }

                // --

                ShowOptions();
                var option = ReadOption();

                ActionRunner.Run(option);
            }
            catch (InvalidOptionException)
            {
                ConsoleWritter.WriteLine("[!!] invalid option", true);

                Console.ReadKey();

                continue;
            }
            catch (ShowErrorMessageException e)
            {
                ConsoleWritter.WriteLine(e.Message, true);

                Console.ReadKey();
            }
            catch (ExitApplicationException)
            {
                ConsoleWritter.WriteLine("good bye!", true);

                break;
            }
            catch (Exception e)
            {
                ConsoleWritter.WriteLine($"[!] {e.Message}", true);

                Console.ReadKey();

                continue;
            }
        }
    }
}