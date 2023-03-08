using GrpcTodo.CLI.Enums;
using GrpcTodo.CLI.Services;
using GrpcTodo.CLI.Utils;

namespace GrpcTodo.CLI;

public sealed class CLI
{
    private readonly string[] _args;
    public readonly Menu Menu;

    public CLI(string[] args)
    {
        _args = args;
        Menu = new Menu();
    }

    public bool HasPossibleCommands()
    {
        return _args.Count() > 0 && _args.Any(x => !x.StartsWith("--"));
    }

    public void ReadArgs()
    {
        foreach (var arg in _args)
        {
            if (arg == "--help")
                Menu.SetArg(arg, true);
        }
    }

    public int GetArgPos(string arg)
    {
        for (int i = 0; i < _args.Count(); i++)
        {
            if (_args[i] == arg)
                return i;
        }

        return -1;
    }

    public async Task Run()
    {
        var commandReader = new CommandReader(Menu, _args);

        try
        {
            var actionRunner = new ActionRunner();

            var menuOption = commandReader.Read();


            if (menuOption is null)
                throw new InvalidCommandException(@$"command ""{commandReader}"" does not exists");

            if (menuOption.Command is not null && GetArgPos(menuOption.Path) == GetArgPos("--help") - 1)
            {
                ConsoleWritter.WriteInfo(Menu.GetCommandHelp(menuOption.Command ?? 0));
                return;
            }

            await actionRunner.Run(menuOption.Command);
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

            Menu.ShowAvailableOptions();

            commandReader.GetNearestCommand(commandReader.ToString());
        }
        catch (Exception e)
        {
            ConsoleWritter.WriteError(e.Message);
        }
    }
}