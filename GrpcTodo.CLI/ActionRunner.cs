using GrpcTodo.CLI.Enums;
using GrpcTodo.CLI.Lib;
using GrpcTodo.CLI.UseCases;

namespace GrpcTodo.CLI;

internal class ActionRunner
{
    private readonly AccountCreateUseCase _createAccountUseCase;
    private readonly AccountLoginUseCase _accountLoginUseCase;
    private readonly AccountLogoutUseCase _accountLogoutUseCase;

    private readonly ConfigsManager _configsManager;
    private readonly Parameters _parameters;

    public ActionRunner(ConfigsManager configsManager, Parameters parameters)
    {
        _configsManager = configsManager;
        _parameters = parameters;

        _createAccountUseCase = new AccountCreateUseCase(_configsManager);
        _accountLoginUseCase = new AccountLoginUseCase(_configsManager);
        _accountLogoutUseCase = new AccountLogoutUseCase(_configsManager);
    }

    public async Task Run(Command? action, string command)
    {
        switch (action)
        {
            case null:
                throw new InvalidCommandException(@$"command ""{command}"" does not exists");
            case Command.Logout:
                _accountLogoutUseCase.Execute();
                break;
            case Command.CreateAccount:
                await _createAccountUseCase.ExecuteAsync(_parameters);
                break;
            case Command.Login:
                await _accountLoginUseCase.ExecuteAsync(_parameters);
                break;
            case Command.ListAllTasks:
                Console.WriteLine("list all tasks");
                break;
            case Command.CreateTask:
                Console.WriteLine("create task");
                break;
            case Command.CompleteTask:
                Console.WriteLine("complete task");
                break;
            case Command.UncompleteTask:
                Console.WriteLine("uncomplete task");
                break;
            case Command.DeleteTask:
                Console.WriteLine("delete task");
                break;
        }
    }
}