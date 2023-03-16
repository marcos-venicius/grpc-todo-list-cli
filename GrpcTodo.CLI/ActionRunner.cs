using GrpcTodo.CLI.Lib;
using GrpcTodo.CLI.Services;
using GrpcTodo.CLI.UseCases.AccountCreate;
using GrpcTodo.CLI.UseCases.AccountLogin;
using GrpcTodo.CLI.UseCases.AccountLogout;
using GrpcTodo.CLI.UseCases.AccountTokenUpdate;
using GrpcTodo.CLI.UseCases.AliasCreate;

namespace GrpcTodo.CLI;

internal class ActionRunner
{
    private readonly AccountCreateUseCase _createAccountUseCase;
    private readonly AccountLoginUseCase _accountLoginUseCase;
    private readonly AccountLogoutUseCase _accountLogoutUseCase;
    private readonly AccountUpdateTokenUseCase _accountUpdateTokenUseCase;
    private readonly AliasCreateUseCase _aliasCreateUseCase;

    private readonly CommandReader _commandReader;
    private readonly ConfigsManager _configsManager;
    private readonly Parameters _parameters;

    public ActionRunner(ConfigsManager configsManager, CommandReader commandReader, Parameters parameters)
    {
        _commandReader = commandReader;
        _configsManager = configsManager;
        _parameters = parameters;

        _createAccountUseCase = new AccountCreateUseCase(_configsManager);
        _accountLoginUseCase = new AccountLoginUseCase(_configsManager);
        _accountLogoutUseCase = new AccountLogoutUseCase(_configsManager);
        _accountUpdateTokenUseCase = new AccountUpdateTokenUseCase(_configsManager);
        _aliasCreateUseCase = new AliasCreateUseCase(_configsManager, commandReader);
    }

    public async Task Run(Command? action)
    {
        switch (action)
        {
            case null:
                throw new InvalidCommandException(@$"command ""{_commandReader}"" does not exists");
            case Command.Logout:
                _accountLogoutUseCase.Execute();
                break;
            case Command.UpdateToken:
                await _accountUpdateTokenUseCase.ExecuteAsync(_parameters);
                break;
            case Command.CreateAccount:
                await _createAccountUseCase.ExecuteAsync(_parameters);
                break;
            case Command.Login:
                await _accountLoginUseCase.ExecuteAsync(_parameters);
                break;
            case Command.CreateAlias:
                _aliasCreateUseCase.Execute();
                break;
            case Command.RemoveAlias:
                Console.WriteLine("not implemented yet: remove an existing alias");
                break;
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
    }
}
