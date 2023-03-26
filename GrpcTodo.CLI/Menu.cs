using GrpcTodo.CLI.Lib;
using GrpcTodo.CLI.Models;
using GrpcTodo.CLI.Utils;

namespace GrpcTodo.CLI;

public sealed class Menu
{
    public static List<MenuOption> Options = new()
    {
        new MenuOption {
            Path = "account",
            IsImplemented = true,

            Children = new () {
                new MenuOption {
                    Description = "create new account. example: gl account create",
                    Path = "create",
                    Command = Command.CreateAccount,
                    IsImplemented = true
                },
                new MenuOption {
                    Path = "login",
                    IsImplemented = true,
                    Command = Command.Login,
                    Description = "make login. example: gl account login"
                },
                new MenuOption {
                    Path = "logout",
                    IsImplemented = true,
                    Command = Command.Logout,
                    Description = "make logout. signout from your account. example: gl account logout"
                },
                new MenuOption {
                    Path = "token",
                    IsImplemented = true,
                    Children = new ()  {
                        new MenuOption {
                            IsImplemented = true,
                            Path = "update",
                            Command = Command.UpdateToken,
                            Description = "hard update your auth token. generate a new one. example: gl account token update"
                        }
                    }
                },
            }
        },

        new MenuOption {
            IsImplemented = true,
            Path = "alias",
            Children = new () {
                new MenuOption {
                    Path = "create",
                    Command = Command.CreateAlias,
                    Description = "create a new alias to any cli command. example: gl alias create",
                    IsImplemented = true
                },
                new MenuOption {
                    IsImplemented = true,
                    Path = "remove",
                    Command = Command.RemoveAlias,
                    Description = "remove an existing alias. example: gl alias remove",
                },
                new MenuOption {
                    Path = "list",
                    Command = Command.ListAliases,
                    Description = "list all available aliases. example: gl alias list",
                    IsImplemented = true
                },
            }
        },

        new MenuOption {
            Path = "task",
            Children = new () {
                new MenuOption {
                    Path = "create",
                    Command = Command.CreateTask,
                    Description = "create a new task"
                },
                new MenuOption {
                    Path = "complete",
                    Command = Command.CompleteTask,
                    Description = "complete a task"
                },
                new MenuOption {
                    Path = "uncomplete",
                    Command = Command.UncompleteTask,
                    Description = "uncomplete a task"
                },
                new MenuOption {
                    Path = "list",
                    Command = Command.ListAllTasks,
                    Description = "list all tasks"
                },
                new MenuOption {
                    Path = "delete",
                    Command = Command.DeleteTask,
                    Description = "delete a task"
                },
            }
        }
    };

    public static string GetCommandHelp(Command command)
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

    private void ShowAvailableOptionsRecursively(List<MenuOption> options, int tabs = 0, bool help = false)
    {
        const int maxSpaceBetweenCommandAndDescription = 30;

        foreach (var option in options)
        {
            if (tabs == 0)
                Console.WriteLine();

            Console.Write(new string(' ', tabs));

            if (option.IsImplemented)
                ConsoleWritter.WriteWithColor(option.Path, ConsoleColor.Green, true);
            else
                ConsoleWritter.WriteWithColor(option.Path, ConsoleColor.Red, true);

            if (help && option.Description is not null)
            {
                var offset = maxSpaceBetweenCommandAndDescription - tabs - option.Path.Length;

                Console.Write(new string(' ', offset));

                Console.Write(option.Description ?? "");
            }

            Console.WriteLine();

            if (option.Children.Any())
                ShowAvailableOptionsRecursively(option.Children, tabs + 2, help);
        }
    }

    public void ShowAvailableOptions(Parameters parameters)
    {
        ConsoleWritter.WriteWithColor("\nAVAILABLE COMMANDS", ConsoleColor.DarkCyan);

        ShowAvailableOptionsRecursively(Options, 0, parameters.Has("--help"));

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
