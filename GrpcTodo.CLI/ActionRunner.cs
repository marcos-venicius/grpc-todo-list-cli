using GrpcTodo.CLI.Enums;

internal static class ActionRunner
{
    public static void Run(ActionType? action)
    {
        switch (action)
        {
            case null:
                return;
            case ActionType.CreateAccount:
                Console.WriteLine("create user account");
                break;
            case ActionType.Login:
                Console.WriteLine("make user login");
                break;
            case ActionType.ListAllTasks:
                Console.WriteLine("list all tasks");
                break;
            case ActionType.CreateTask:
                Console.WriteLine("create task");
                break;
            case ActionType.CompleteTask:
                Console.WriteLine("complete task");
                break;
            case ActionType.UncompleteTask:
                Console.WriteLine("uncomplete task");
                break;
            case ActionType.DeleteTask:
                Console.WriteLine("delete task");
                break;
        }
    }
}