using GrpcTodo.CLI.Enums;
using GrpcTodo.CLI.Models;
using GrpcTodo.CLI.Utils;

namespace GrpcTodo.CLI;

public sealed class Menu
{
    private readonly Dictionary<string, object> _args = new();

    public List<MenuOption> Options = new()
    {
        new MenuOption {
            Path = "account",
            IsImplemented = true,

            Children = new () {
                new MenuOption {
                    Description = "create new account",
                    Path = "create",
                    Command = Command.CreateAccount,
                    Children = new (),
                    IsImplemented = true
                },
                new MenuOption {
                    Path = "login",
                    Command = Command.Login,
                    Description = "make login",
                    Children = new()
                }
            }
        },

        new MenuOption {
            Path = "task",
            Children = new () {
                new MenuOption {
                    Path = "create",
                    Command = Command.CreateTask,
                    Description = "create a new task",
                    Children = new()
                },
                new MenuOption {
                    Path = "complete",
                    Command = Command.CompleteTask,
                    Description = "complete a task",
                    Children = new()
                },
                new MenuOption {
                    Path = "uncomplete",
                    Command = Command.UncompleteTask,
                    Description = "uncomplete a task",
                    Children = new()
                },
                new MenuOption {
                    Path = "list",
                    Command = Command.ListAllTasks,
                    Description = "list all tasks",
                    Children = new()
                },
                new MenuOption {
                    Path = "delete",
                    Command = Command.DeleteTask,
                    Description = "delete a task",
                    Children = new()
                },
            }
        }
    };

    public void SetArg(string arg, object value)
    {
        _args.Add(arg, value);
    }

    public object? GetArg(string arg)
    {
        _args.TryGetValue(arg, out var data);

        return data;
    }

    public string GetCommandHelp(Command command)
    {
        MenuOption? Find(List<MenuOption> options)
        {
            foreach (var option in options)
            {
                if (option.Command == command)
                    return option;

                if (option.Children.Any())
                {
                    var result = Find(option.Children);

                    if (result is not null)
                        return result;
                }
            }

            return default!;
        }

        var menuOption = Find(Options);

        if (menuOption is null)
            return "COMMAND HELP NOT FOUND";

        return @$"
description: {menuOption.Description}
";
    }

    private void ShowAvailableOptionsRecursively(List<MenuOption> options, int tabs = 0)
    {
        var help = _args.ContainsKey("--help");

        foreach (var option in options)
        {
            string tab = new string(' ', tabs);

            string path = $"{tab} {option.Path}";

            if (tabs == 0)
            {
                Console.WriteLine();
            }

            if (option.IsImplemented)
                ConsoleWritter.WriteWithColor(path, ConsoleColor.Green, option.Description is null);
            else
                ConsoleWritter.WriteWithColor(path, ConsoleColor.Red, option.Description is null);

            if (help && option.Description is not null)
                ConsoleWritter.Write($@"{new string(' ', tabs)}{option.Description}", true);
            else if (!help && option.Description is not null)
                Console.WriteLine();

            if (option.Children.Any())
                ShowAvailableOptionsRecursively(option.Children, tabs + 2);
        }
    }

    public void ShowAvailableOptions()
    {
        ConsoleWritter.WriteWithColor("\nAVAILABLE COMMANDS:\n", ConsoleColor.DarkCyan);

        ShowAvailableOptionsRecursively(Options);

        Console.WriteLine();
    }

    private static List<string> GetMenuOptionsPaths(MenuOption option)
    {
        List<string> paths = new();

        void ExtractPath(List<MenuOption> options, string path = "")
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