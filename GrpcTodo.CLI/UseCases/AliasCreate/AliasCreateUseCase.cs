using GrpcTodo.CLI.Lib;
using GrpcTodo.CLI.Services;
using GrpcTodo.CLI.UseCases.Common;
using GrpcTodo.CLI.Utils;

namespace GrpcTodo.CLI.UseCases.AliasCreate;

public sealed class AliasCreateUseCase : UseCase
{
    private readonly CommandReader _commandReader;

    public AliasCreateUseCase(ConfigsManager configsManager, CommandReader commandReader) : base(configsManager)
    {
        _commandReader = commandReader;
    }

    public override Task ExecuteAsync()
    {
        var allAliasesInConfigFile = _commandReader.LoadAliases();

        var commandsWithAlias = new Dictionary<string, bool>();

        foreach (var alias in allAliasesInConfigFile.Keys)
            commandsWithAlias.Add(allAliasesInConfigFile[alias], true);

        var commandsWithPaths = _commandReader.GetCommandsWithPaths();

        var prompt = new AliasCreatePrompt();

        var command = prompt.PromptCommand(commandsWithPaths, commandsWithAlias);
        var aliasname = prompt.PromptAlias();

        ConsoleWritter.WriteInfo($@"can use alias ""{aliasname}""? ", true);

        if (allAliasesInConfigFile.TryGetValue(aliasname, out string? commandAlias))
        {
            ConsoleWritter.WriteWithColor("no", ConsoleColor.Red);
            ConsoleWritter.WriteError(@$"this alias already exists to the command ""{commandAlias}""");
            return Task.CompletedTask;
        }

        ConsoleWritter.WriteWithColor("yes", ConsoleColor.Green);

        foreach (var (path, option) in commandsWithPaths)
        {
            if (option.Command == command)
            {
                ConsoleWritter.WriteInfo(@$"creating alias ""{aliasname}"" to command ""{path}""");

                _configsManager.SetItem(ConfigKey.Alias, aliasname, path);

                ConsoleWritter.WriteSuccess("alias created successfully");

                return Task.CompletedTask;
            }
        }

        ConsoleWritter.WriteError("cannot found command path");

        return Task.CompletedTask;
    }
}