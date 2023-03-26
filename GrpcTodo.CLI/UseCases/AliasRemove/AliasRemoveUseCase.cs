using GrpcTodo.CLI.Lib;
using GrpcTodo.CLI.Services;
using GrpcTodo.CLI.UseCases.Common;
using GrpcTodo.CLI.Utils;

namespace GrpcTodo.CLI.UseCases.AliasRemove;

public sealed class AliasRemoveUseCase : UseCase
{
    private readonly CommandReader _commandReader;

    public AliasRemoveUseCase(ConfigsManager configsManager, CommandReader commandReader) : base(configsManager)
    {
        _commandReader = commandReader;
    }

    public override Task ExecuteAsync()
    {
        var aliases = _commandReader.LoadAliases();

        if (!aliases.Any())
        {
            ConsoleWritter.Write("you have no aliases");

            return Void;
        }

        var prompt = new AliasRemovePrompt();

        var aliasName = prompt.PromptAlias(aliases);

        ConsoleWritter.WriteInfo($@"can remove alias ""{aliasName}""? ", true);

        if (!aliases.ContainsKey(aliasName))
        {
            ConsoleWritter.WriteWithColor("no", ConsoleColor.Red);

            ConsoleWritter.WriteError("this alias does not exists");

            return Void;
        }

        ConsoleWritter.WriteWithColor("yes", ConsoleColor.Green);

        ConsoleWritter.WriteInfo($@"removing alias ""{aliasName}""");

        _configsManager.RemoveItem(ConfigKey.Alias, aliasName);

        ConsoleWritter.WriteSuccess($@"alias ""{aliasName}"" removed successfully");

        return Void;
    }
}