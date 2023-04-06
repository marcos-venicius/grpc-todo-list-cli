using GrpcTodo.Server.Domain.Repositories;

namespace GrpcTodo.Server.Domain.UseCases.Tasks;

public sealed class ListAllTasksUseCase
{
    private readonly ITaskRepository _taskRepository;

    public ListAllTasksUseCase(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<List<ListAllTasksUseCaseOutput>> ExecuteAsync(Guid userId)
    {
        var tasks = new List<ListAllTasksUseCaseOutput>();

        var list = await _taskRepository.ListAsync(userId);

        foreach (var task in list)
            tasks.Add(new ListAllTasksUseCaseOutput(task.Id.ToString(), task.Name, task.Completed, task.CreatedAt.Ticks));

        return tasks;
    }
}

public sealed record ListAllTasksUseCaseOutput(string Id, string Name, bool Completed, long CreatedAt);
