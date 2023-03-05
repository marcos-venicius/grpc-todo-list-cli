using Grpc.Core;
using GrpcTodo.Server.Domain.UseCases.User;
using GrpcTodo.SharedKernel.Protos.User;
using GrpcTodo.SharedKernel.Protos.User.Requests;
using GrpcTodo.SharedKernel.Protos.User.Responses;

namespace GrpcTodo.Server.GrpcServices;

public sealed class UserGrpcService : User.UserBase
{
    private readonly CreateUserUseCase _createUserUseCase;

    public UserGrpcService(CreateUserUseCase createUserUseCase)
    {
        _createUserUseCase = createUserUseCase;
    }

    public override async Task<UserCreateResponse> Create(UserCreateRequest request, ServerCallContext context)
    {
        var input = new CreateUserUseCaseInput(request.Name, request.Password);

        await _createUserUseCase.ExecuteAsync(input);

        return new UserCreateResponse { Token = "" };
    }
}