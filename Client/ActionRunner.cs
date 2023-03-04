using Client.Enums;
using Client.Exceptions;
using Client.UseCases;

internal static class ActionRunner
{
    public static void Run(ActionType action)
    {
        switch (action)
        {
            case ActionType.None:
                return;
            case ActionType.ListTodo:
                new ListTodosUseCase().Execute();
                break;
            case ActionType.CreateTodo:
                new CreateTodoUseCase().Execute();
                break;
            case ActionType.Exit:
                throw new ExitApplicationException();
        }
    }
}