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
        }
    }
}