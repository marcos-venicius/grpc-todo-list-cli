using GrpcTodo.CLI.Enums;
using GrpcTodo.CLI.Models;
using GrpcTodo.CLI.Utils;

namespace GrpcTodo.CLI;

public sealed class Menu
{
    public List<Option> Options = new()
    {
        new Option {
            Path = "task",
            Children = new () {
                new Option {
                    Path = "create",
                    Command = Command.CreateTask,
                    Description = "create a new task",
                    Children = new()
                },
                new Option {
                    Path = "complete",
                    Command = Command.CompleteTask,
                    Description = "complete a task",
                    Children = new()
                },
                new Option {
                    Path = "uncomplete",
                    Command = Command.UncompleteTask,
                    Description = "uncomplete a task",
                    Children = new()
                },
                new Option {
                    Path = "list",
                    Command = Command.ListAllTasks,
                    Description = "list all tasks",
                    Children = new()
                },
                new Option {
                    Path = "delete",
                    Command = Command.DeleteTask,
                    Description = "delete a task",
                    Children = new()
                },
            }
        },
        new Option {
            Path = "account",
            Children = new () {
                new Option {
                    Description = "create new account",
                    Path = "create",
                    Command = Command.CreateAccount,
                    Children = new ()
                },
                new Option {
                    Path = "login",
                    Command = Command.Login,
                    Description = "make login",
                    Children = new()
                }
            }
        },
    };

    private static void ShowAvailableOptionsRecursively(List<Option> options, int tabs = 0)
    {
        foreach (var option in options)
        {
            string tab = new string(' ', tabs);

            string message = $"{tab} {option.Path}";

            if (tabs == 0)
            {
                Console.WriteLine();
            }

            ConsoleWritter.WriteInfo(message, option.Description is null);

            if (option.Description is not null)
                ConsoleWritter.Write($@"{new string(' ', tabs)}{option.Description}", true);

            if (option.Children.Any())
                ShowAvailableOptionsRecursively(option.Children, tabs + 2);
        }
    }

    public void ShowAvailableOptions()
    {
        ConsoleWritter.Write("AVAILABLE COMMANDS:");

        ShowAvailableOptionsRecursively(Options);

        Console.WriteLine();
    }

    private static List<string> GetMenuOptionsPaths(Option option)
    {
        List<string> paths = new();

        void ExtractPath(List<Option> options, string path = "")
        {
            if (option.Path != path)
                paths.Add(path);

            foreach (var option in options)
            {
                var _path = $"{path} {option.Path}";

                ExtractPath(option.Children, _path);
            }
        }

        ExtractPath(option.Children, option.Path);

        return paths;
    }

    public List<string> GetMenuCommands()
    {
        List<string> commandsPath = new();

        foreach (var option in Options)
        {
            var menuOptionPaths = GetMenuOptionsPaths(option);

            commandsPath.AddRange(menuOptionPaths);
        }

        return commandsPath;
    }
}