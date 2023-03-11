using GrpcTodo.CLI.Enums;
using GrpcTodo.CLI.Lib;
using GrpcTodo.CLI.UseCases;

namespace GrpcTodo.CLI;

internal class ActionRunner
{
    private readonly CreateAccountUseCase _createAccountUseCase;
    private readonly ConfigsManager _configsManager;
    private readonly Parameters _parameters;

    public ActionRunner(ConfigsManager configsManager, Parameters parameters)
    {
        _configsManager = configsManager;
        _parameters = parameters;

        _createAccountUseCase = new CreateAccountUseCase(_configsManager);
    }

    public async Task Run(Command? action, string command)
    {
        switch (action)
        {
            case null:
                throw new InvalidCommandException(@$"command ""{command}"" does not exists");
            case Command.CreateAccount:
                await _createAccountUseCase.ExecuteAsync(_parameters);
                break;
            case Command.Login:
                Console.WriteLine("make user login");
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