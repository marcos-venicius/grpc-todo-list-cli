using GrpcTodo.CLI.Lib;
using GrpcTodo.CLI.Services;
using GrpcTodo.CLI.UseCases.AccountCreate;
using GrpcTodo.CLI.UseCases.AccountLogin;
using GrpcTodo.CLI.UseCases.AccountLogout;
using GrpcTodo.CLI.UseCases.AccountTokenUpdate;
using GrpcTodo.CLI.UseCases.AliasCreate;
using GrpcTodo.CLI.UseCases.AliasList;
using GrpcTodo.CLI.UseCases.AliasRemove;

namespace GrpcTodo.CLI;

internal class ActionRunner
{
    private readonly AccountCreateUseCase _createAccountUseCase;
    private readonly AccountLoginUseCase _accountLoginUseCase;
    private readonly AccountLogoutUseCase _accountLogoutUseCase;
    private readonly AccountUpdateTokenUseCase _accountUpdateTokenUseCase;
    private readonly AliasCreateUseCase _aliasCreateUseCase;
    private readonly AliasListUseCase _aliasListUseCase;
    private readonly AliasRemoveUseCase _aliasRemoveUseCase;

    private readonly CommandReader _commandReader;

    public ActionRunner(ConfigsManager configsManager, CommandReader commandReader, Parameters parameters)
    {
        _commandReader = commandReader;

        _createAccountUseCase = new AccountCreateUseCase(configsManager, parameters);
        _accountLoginUseCase = new AccountLoginUseCase(configsManager, parameters);
        _accountLogoutUseCase = new AccountLogoutUseCase(configsManager);
        _accountUpdateTokenUseCase = new AccountUpdateTokenUseCase(configsManager, parameters);
        _aliasCreateUseCase = new AliasCreateUseCase(configsManager, commandReader);
        _aliasListUseCase = new AliasListUseCase(configsManager, commandReader);
        _aliasRemoveUseCase = new AliasRemoveUseCase(configsManager, commandReader);
    }

    public Task Run(Command? action)
    {
        switch (action)
        {
            case null:
                throw new InvalidCommandException(@$"command/alias ""{_commandReader}"" does not exists");
            case Command.Logout:
                return _accountLogoutUseCase.ExecuteAsync();
            case Command.UpdateToken:
                return _accountUpdateTokenUseCase.ExecuteAsync();
            case Command.CreateAccount:
                return _createAccountUseCase.ExecuteAsync();
            case Command.Login:
                return _accountLoginUseCase.ExecuteAsync();
            case Command.CreateAlias:
                return _aliasCreateUseCase.ExecuteAsync();
            case Command.ListAliases:
                return _aliasListUseCase.ExecuteAsync();
            case Command.RemoveAlias:
                return _aliasRemoveUseCase.ExecuteAsync();
            case Command.ListAllTasks:
                Console.WriteLine("no implemented yet: list all tasks");
                break;
            case Command.CreateTask:
                Console.WriteLine("no implemented yet: create task");
                break;
            case Command.CompleteTask:
                Console.WriteLine("no implemented yet: complete task");
                break;
            case Command.UncompleteTask:
                Console.WriteLine("no implemented yet: uncomplete task");
                break;
            case Command.DeleteTask:
                Console.WriteLine("no implemented yet: delete task");
                break;
        }

        return Task.CompletedTask;
    }
}
