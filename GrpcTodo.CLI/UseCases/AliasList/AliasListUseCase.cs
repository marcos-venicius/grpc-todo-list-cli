using GrpcTodo.CLI.Lib;
using GrpcTodo.CLI.Services;
using GrpcTodo.CLI.UseCases.Common;
using GrpcTodo.CLI.Utils;

namespace GrpcTodo.CLI.UseCases.AliasList;
public class AliasListUseCase : UseCase
{
    private readonly CommandReader _commandReader;

    public AliasListUseCase(ConfigsManager configsManager, CommandReader commandReader) : base(configsManager)
    {
        _commandReader = commandReader;
    }

    public override Task ExecuteAsync()
    {
        ConsoleWritter.WriteInfo("AVAILABLE ALIASES\n");

        var aliases = _commandReader.LoadAliases();

        foreach (var alias in aliases.Keys)
            ConsoleWritter.WriteWithColor($"{alias,-30}{aliases[alias],-30}", ConsoleColor.White);

        if (!aliases.Any())
            ConsoleWritter.Write("you not have any aliases");

        Console.WriteLine();

        return Empty();
    }
}
