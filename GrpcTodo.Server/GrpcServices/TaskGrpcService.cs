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

    public TaskGrpcService(
        CreateTaskUseCase createTaskUseCase,
        IAuthMiddleware authMiddleware)
    {
        _createTaskUseCase = createTaskUseCase;
        _authMiddleware = authMiddleware;
    }

    public override async Task<TaskCreateResponse> Create(TaskCreateRequest request, ServerCallContext context)
    {
        // TODO: get credentials from request
        var credentials = new Credentials(Guid.NewGuid());

        var input = new CreateTaskUseCaseInput(request.Name);

        var response = await _authMiddleware.Authenticate(credentials, input, _createTaskUseCase.ExecuteAsync);

        return new TaskCreateResponse
        {
            Id = response.ToString()
        };
    }
}
