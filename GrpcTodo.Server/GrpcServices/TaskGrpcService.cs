using Grpc.Core;

using GrpcTodo.Server.Application.Common;
using GrpcTodo.Server.Domain.Middleware;
using GrpcTodo.Server.Domain.UseCases.Tasks;
using GrpcTodo.SharedKernel.Protos.Tasks;
using GrpcTodo.SharedKernel.Protos.Tasks.Requests;
using GrpcTodo.SharedKernel.Protos.Tasks.Responses;

namespace GrpcTodo.Server.GrpcServices;

public sealed class TaskGrpcService : TaskItem.TaskItemBase
{
    private readonly IAuthMiddleware _authMiddleware;
    private readonly CreateTaskUseCase _createTaskUseCase;
    private readonly ListAllTasksUseCase _listAllTasksUseCase;

    public TaskGrpcService(
        CreateTaskUseCase createTaskUseCase,
        ListAllTasksUseCase listAllTasksUseCase,
        IAuthMiddleware authMiddleware)
    {
        _createTaskUseCase = createTaskUseCase;
        _listAllTasksUseCase = listAllTasksUseCase;
        _authMiddleware = authMiddleware;
    }

    public override async Task<TaskCreateResponse> Create(TaskCreateRequest request, ServerCallContext context)
    {
        var credentials = new Credentials(request.AccessToken);

        var input = new CreateTaskUseCaseInput(request.Name);

        var response = await _authMiddleware.Authenticate(credentials, input, _createTaskUseCase.ExecuteAsync);

        return new TaskCreateResponse
        {
            Id = response.ToString()
        };
    }

    public override async Task<TaskListResponse> ListAll(TaskListRequest request, ServerCallContext context)
    {
        var credentials = new Credentials(request.AccessToken);

        var response = await _authMiddleware.Authenticate(credentials, _listAllTasksUseCase.ExecuteAsync);

        var tasks = new TaskListResponse();

        foreach (var task in response)
        {
            tasks.Items.Add(new TaskListResponseItem
            {
                CreatedAt = task.CreatedAt,
                Completed = task.Completed,
                Name = task.Name,
                Id = task.Id,
            });
        }

        return tasks;
    }
}
