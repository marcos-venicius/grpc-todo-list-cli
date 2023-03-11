using GrpcTodo.CLI.Enums;
using GrpcTodo.CLI.Lib;
using GrpcTodo.CLI.UseCases;

internal class ActionRunner
{
    private readonly CreateAccountUseCase _createAccountUseCase;

    public ActionRunner()
    {
        _createAccountUseCase = new CreateAccountUseCase();
    }

    public async Task Run(Command? action, string command, Parameters parameters)
    {
        switch (action)
        {
            case null:
                throw new InvalidCommandException(@$"command ""{command}"" does not exists");
            case Command.CreateAccount:
                await _createAccountUseCase.ExecuteAsync(parameters);
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