using GrpcTodo.CLI.Enums;
using GrpcTodo.CLI.UseCases;

internal class ActionRunner
{
    private readonly CreateAccountUseCase _createAccountUseCase;

    public ActionRunner()
    {
        _createAccountUseCase = new CreateAccountUseCase();
    }

    public async Task Run(Command? action)
    {
        switch (action)
        {
            case null:
                break;
            case Command.CreateAccount:
                await _createAccountUseCase.ExecuteAsync();
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