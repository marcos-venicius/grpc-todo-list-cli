using GrpcTodo.CLI.Lib;
using GrpcTodo.CLI.Services;
using GrpcTodo.CLI.UseCases.Common;

namespace GrpcTodo.CLI.UseCases.AliasCreate;

public sealed class AliasCreateUseCase : UseCase
{
    private readonly CommandReader _commandReader;

    public AliasCreateUseCase(ConfigsManager configsManager, CommandReader commandReader) : base(configsManager)
    {
        _commandReader = commandReader;
    }

    public void Execute()
    {
        var allAliasesInConfigFile = _commandReader.LoadAliases();

        var commandsWithAlias = new Dictionary<string, bool>();

        foreach (var alias in allAliasesInConfigFile.Keys)
            commandsWithAlias.Add(allAliasesInConfigFile[alias], true);

        var commandsWithPaths = _commandReader.GetCommandsWithPaths();

        var prompt = new AliasCreatePrompt();

        var command = prompt.PromptCommand(commandsWithPaths, commandsWithAlias);

        Console.WriteLine($"choosed {command}");
    }
}