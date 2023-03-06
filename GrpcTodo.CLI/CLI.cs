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

    public void Run()
    {
        var commandReader = new CommandReader(Menu, _args);

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

            Menu.ShowAvailableOptions();

            commandReader.GetNearestCommand(commandReader.ToString());
        }
        catch (Exception e)
        {
            ConsoleWritter.WriteError(e.Message);
        }
    }
}