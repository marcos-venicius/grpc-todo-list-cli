using GrpcTodo.CLI.Enums;
using GrpcTodo.CLI.Models;
using GrpcTodo.CLI.Services;
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
                    Action = Command.CreateTask,
                    Name = "create a new task",
                    Children = new()
                },
                new Option {
                    Path = "complete",
                    Action = Command.CompleteTask,
                    Name = "complete task",
                    Children = new()
                },
                new Option {
                    Path = "uncomplete",
                    Action = Command.UncompleteTask,
                    Name = "uncomplete task",
                    Children = new()
                },
                new Option {
                    Path = "list",
                    Action = Command.ListAllTasks,
                    Name = "list all tasks",
                    Children = new()
                },
                new Option {
                    Path = "delete",
                    Action = Command.DeleteTask,
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
                    Action = Command.CreateAccount,
                    Children = new ()
                },
                new Option {
                    Path = "login",
                    Action = Command.Login,
                    Name = "make login",
                    Children = new()
                }
            }
        },
    };

    private static Command? ReadNaturalLanguageCommand(string[] args)
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

        var commandReader = new CommandReader(Options, args);

        try
        {
            var command = commandReader.Read();

            if (command is null)
                throw new InvalidCommandException(@$"command ""{commandReader}"" does not exists");

            ActionRunner.Run(command);
        }
        catch (ShowErrorMessageException e)
        {
            ConsoleWritter.WriteError(e.Message);
        }
        catch (ExitApplicationException)
        {
            ConsoleWritter.WriteSuccess("good bye!");
        }
        catch (InvalidCommandException e)
        {
            ConsoleWritter.WriteError(e.Message);

            commandReader.ShowAvailableCommands();

            commandReader.GetNearestCommand(commandReader.ToString());
        }
        catch (Exception e)
        {
            ConsoleWritter.WriteError(e.Message);
        }
    }
}