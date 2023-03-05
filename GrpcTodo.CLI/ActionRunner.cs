using GrpcTodo.CLI.Enums;

internal static class ActionRunner
{
    public static void Run(Command? action)
    {
        switch (action)
        {
            case null:
                return;
            case Command.CreateAccount:
                Console.WriteLine("create user account");
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