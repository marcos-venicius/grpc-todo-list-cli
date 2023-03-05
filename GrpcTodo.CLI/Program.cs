using GrpcTodo.CLI.Enums;
using GrpcTodo.CLI.Models;
using GrpcTodo.CLI.Utils;

public static class Program
{
    private static List<Option> Options = new()
    {
        new Option{
            Path = "create",
            Children = new List<Option> {
                new Option {
                    Name = "create account",
                    Path = "account",
                    Action = ActionType.CreateAccount,
                    Children = new List<Option>()
                }
            }
        }
    };

    private static ActionType? IndentifyCommand(string[] args)
    {
        var rootOptions = Options;
        var currentArgPos = 0;
        var currentOptionPos = 0;

        while (currentOptionPos < rootOptions.Count)
        {
            var option = rootOptions[currentOptionPos]; // account

            if (option.Path == args[currentArgPos])
            {
                if (currentArgPos == args.Length - 1)
                {
                    var ac = option.Action;

                    return ac;
                }

                rootOptions = option.Children;
                currentOptionPos = 0;
                currentArgPos++;
            }
            else
            {
                currentOptionPos++;
            }
        }

        return null;
    }

    public static void Main(string[] args)
    {
        string readableCommand = string.Join(' ', args);

        try
        {
            var action = IndentifyCommand(args);

            if (action is null)
                throw new Exception(@$"command ""{readableCommand}"" does not exists");

            ActionRunner.Run(action);
        }
        catch (InvalidOptionException)
        {
            ConsoleWritter.WriteLine("[!!] invalid option", true);
        }
        catch (ShowErrorMessageException e)
        {
            ConsoleWritter.WriteLine(e.Message, true);
        }
        catch (ExitApplicationException)
        {
            ConsoleWritter.WriteLine("good bye!", true);
        }
        catch (Exception e)
        {
            ConsoleWritter.WriteLine($"[!] {e.Message}", true);
        }
    }
}