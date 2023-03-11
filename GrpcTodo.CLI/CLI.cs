using GrpcTodo.CLI.Lib;
using GrpcTodo.CLI.Models;
using GrpcTodo.CLI.Services;
using GrpcTodo.CLI.Utils;

namespace GrpcTodo.CLI;

public sealed class CLI
{
    private readonly string[] _args;
    private readonly ArgsParams _argsParams;
    public readonly Menu Menu;

    public CLI(string[] args)
    {
        _args = args;
        _argsParams = new ArgsParams(args);
        Menu = new Menu();

        _argsParams.Set(
            "--help",
            new ParamDetail("Help", "Get command help")
        );
    }

    public async Task Run()
    {
        var parameters = _argsParams.Read();

        var commandReader = new CommandReader(Menu, _args);

        try
        {
            var actionRunner = new ActionRunner();

            if (commandReader.HasPossibleOptions())
            {
                var menuOption = commandReader.Read();

                await actionRunner.Run(menuOption?.Command, commandReader.ToString(), parameters);
            }
            else
            {
                Menu.ShowAvailableOptions(parameters);
            }
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

            Menu.ShowAvailableOptions(parameters);

            commandReader.GetNearestCommand(commandReader.ToString());
        }
        catch (Exception e)
        {
            ConsoleWritter.WriteError(e.Message);
        }
    }
}
