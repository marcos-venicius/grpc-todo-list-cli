using GrpcTodo.Server.Domain.Repositories;

namespace GrpcTodo.Server.Domain.UseCases.Tasks;

public sealed class DeleteTaskUseCase
{
    private readonly ITaskRepository _taskRepository;

    public DeleteTaskUseCase(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task ExecuteAsync(Guid userId, DeleteTaskUseCaseInput request)
    {
        var task = await _taskRepository.FindByShortIdAsync(request.Id);

        if (task is null)
            throw new NotFoundException("this task does not exists");

        if (task.UserId != userId)
            throw new DeniedException("this task does not belongs to you");

        await _taskRepository.DeleteAsync(task.Id);
    }
}

public sealed record DeleteTaskUseCaseInput(string Id);