using GrpcTodo.CLI.Enums;
using GrpcTodo.CLI.Models;
using GrpcTodo.CLI.Utils;

public static class Program
{
    private static List<Option> Options = new()
    {
        new Option {
            Path = "task",
            Children = new () {
                new Option {
                    Path = "create",
                    Action = ActionType.CreateTask,
                    Name = "create a new task",
                    Children = new()
                },
                new Option {
                    Path = "complete",
                    Action = ActionType.CompleteTask,
                    Name = "complete task",
                    Children = new()
                },
                new Option {
                    Path = "uncomplete",
                    Action = ActionType.UncompleteTask,
                    Name = "uncomplete task",
                    Children = new()
                },
                new Option {
                    Path = "list",
                    Action = ActionType.ListAllTasks,
                    Name = "list all tasks",
                    Children = new()
                },
                new Option {
                    Path = "delete",
                    Action = ActionType.DeleteTask,
                    Name = "delete a task",
                    Children = new()
                },
            }
        },

        new Option {
            Path = "account",
            Children = new () {
                new Option {
                    Name = "create account",
                    Path = "create",
                    Action = ActionType.CreateAccount,
                    Children = new ()
                },
                new Option {
                    Path = "login",
                    Action = ActionType.Login,
                    Name = "make login",
                    Children = new()
                }
            }
        },
    };

    private static ActionType? ReadNaturalLanguageCommand(string[] args)
    {
        var rootOptions = Options;
        var currentArgPos = 0;
        var currentOptionPos = 0;

        while (currentOptionPos < rootOptions.Count)
        {
            var option = rootOptions[currentOptionPos];

            if (option.Path == args[currentArgPos])
            {
                if (currentArgPos == args.Length - 1)
                    return option.Action;

                rootOptions = option.Children;
                currentOptionPos = 0;
                currentArgPos++;
            }
            else
                currentOptionPos++;
        }

        return null;
    }

    public static void Main(string[] args)
    {
        if (args.Length == 0)
            return;

        try
        {
            var action = ReadNaturalLanguageCommand(args);

            if (action is null)
            {
                string readableCommand = string.Join(' ', args);

                throw new Exception(@$"command ""{readableCommand}"" does not exists");
            }

            ActionRunner.Run(action);
        }
        catch (InvalidOptionException)
        {
            ConsoleWritter.WriteLine("[!!] invalid option");
        }
        catch (ShowErrorMessageException e)
        {
            ConsoleWritter.WriteLine(e.Message);
        }
        catch (ExitApplicationException)
        {
            ConsoleWritter.WriteLine("good bye!");
        }
        catch (Exception e)
        {
            ConsoleWritter.WriteLine($"[!] {e.Message}");
        }
    }
}