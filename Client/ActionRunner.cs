using Client.Enums;

internal static class ActionRunner
{
    public static void Run(ActionType action)
    {
        switch (action)
        {
            case ActionType.None:
                return;
            case ActionType.ListTodo:
                break;
            case ActionType.CreateTodo:
                break;
            case ActionType.Exit:
                throw new ExitApplicationException();
        }
    }
}