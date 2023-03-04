using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace Server.Services;

public static class Db
{
    public static Google.Protobuf.Collections.RepeatedField<TodoItem> Todos { get; set; } = new();
}

public sealed class TodoService : Todo.TodoBase
{
    private readonly ILogger<TodoService> _logger;

    public TodoService(ILogger<TodoService> logger)
    {
        _logger = logger;
    }

    public override Task<TodoList> List(Empty request, ServerCallContext context)
    {
        var result = new TodoList();

        result.Items.Add(Db.Todos);

        return Task.FromResult(result);
    }

    public override Task<CreateTodoResponse> Create(CreateTodoRequest request, ServerCallContext context)
    {
        var todoId = Guid.NewGuid().ToString();

        Db.Todos.Add(new TodoItem
        {
            Id = todoId,
            Description = request.Description,
            Name = request.Name,
            State = TodoItemState.Uncompleted
        });

        _logger.LogInformation("todo {name} : {description} created", request.Name, request.Description);

        return Task.FromResult(new CreateTodoResponse
        {
            Id = todoId
        });
    }
}