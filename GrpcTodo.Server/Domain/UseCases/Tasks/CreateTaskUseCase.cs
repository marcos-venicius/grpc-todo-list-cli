using GrpcTodo.Server.Domain.Repositories;
using GrpcTodo.Server.Domain.Services;

namespace GrpcTodo.Server.Domain.UseCases.Tasks;

public sealed class CreateTaskUseCase
{
    private readonly IGuidGenerator _guidGenerator;
    private readonly IUserRepository _userRepository;
    private readonly ITaskRepository _taskRepository;

    public CreateTaskUseCase(
        IUserRepository userRepository,
        ITaskRepository taskRepository,
        IGuidGenerator guidGenerator)
    {
        _userRepository = userRepository;
        _taskRepository = taskRepository;
        _guidGenerator = guidGenerator;
    }

    private void Validate(CreateTaskUseCaseInput input)
    {
        if (string.IsNullOrWhiteSpace(input.Name))
            throw new InvalidTaskNameException("invalid task name");
    }

    public async Task<Guid> ExecuteAsync(Guid userId, CreateTaskUseCaseInput input)
    {
        Validate(input);

        var userOnDatabase = await _userRepository.FindByIdAsync(userId);

        if (userOnDatabase is null)
            throw new NotFoundException("this user does not exists");

        var taskWithSameName = await _taskRepository.FindByNameAsync(input.Name.Trim().ToLower());

        if (taskWithSameName is not null)
            throw new ConflictException("already exists a task with same name");

        var guid = _guidGenerator.Generate();

        await _taskRepository.CreateAsync(guid, input.Name, userId);

        return guid;
    }
}

public sealed record CreateTaskUseCaseInput(string Name);
